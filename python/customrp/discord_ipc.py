"""A minimal Discord RPC client speaking the local IPC protocol.

Discord listens on a Unix domain socket (Linux, macOS) or a named pipe (Windows) called
``discord-ipc-N`` with N from 0 to 9. Every message is a frame: a little-endian int32 opcode,
a little-endian int32 payload length, then a UTF-8 JSON payload.

All socket I/O happens on one worker thread. That matters on Windows, where a blocking read
on a synchronous pipe handle would also block writes to it from another thread.
"""

import json
import logging
import os
import queue
import select
import socket
import struct
import sys
import threading
import time
import uuid

OP_HANDSHAKE = 0
OP_FRAME = 1
OP_CLOSE = 2
OP_PING = 3
OP_PONG = 4

HEADER = struct.Struct("<ii")
MAX_PAYLOAD = 64 * 1024 * 1024

log = logging.getLogger("customrp.rpc")


# --- Transports ----------------------------------------------------------------

class SocketTransport:
    """A connected stream socket (a Unix socket in practice, any socket in tests)."""

    def __init__(self, sock: socket.socket):
        self.sock = sock

    def send(self, data: bytes):
        self.sock.sendall(data)

    def poll(self, timeout: float) -> bool:
        readable, _, _ = select.select([self.sock], [], [], timeout)
        return bool(readable)

    def recv(self, size: int) -> bytes:
        chunks = []
        while size > 0:
            chunk = self.sock.recv(size)
            if not chunk:
                raise ConnectionError("Discord closed the connection")
            chunks.append(chunk)
            size -= len(chunk)
        return b"".join(chunks)

    def close(self):
        try:
            self.sock.close()
        except OSError:
            pass


class PipeTransport:
    """A Windows named pipe opened in synchronous mode; polling is done with PeekNamedPipe."""

    def __init__(self, path: str):
        import ctypes
        import msvcrt
        from ctypes import wintypes

        self.file = open(path, "r+b", buffering=0)
        self.handle = msvcrt.get_osfhandle(self.file.fileno())
        self._ctypes = ctypes
        self._peek = ctypes.windll.kernel32.PeekNamedPipe
        self._peek.argtypes = [wintypes.HANDLE, wintypes.LPVOID, wintypes.DWORD,
                               wintypes.LPVOID, ctypes.POINTER(wintypes.DWORD), wintypes.LPVOID]
        self._peek.restype = wintypes.BOOL
        self._available = wintypes.DWORD()

    def send(self, data: bytes):
        self.file.write(data)

    def poll(self, timeout: float) -> bool:
        deadline = time.monotonic() + timeout
        while True:
            if not self._peek(self.handle, None, 0, None, self._ctypes.byref(self._available), None):
                raise ConnectionError("Discord closed the connection")
            if self._available.value:
                return True
            if time.monotonic() >= deadline:
                return False
            time.sleep(0.02)

    def recv(self, size: int) -> bytes:
        chunks = []
        while size > 0:
            chunk = self.file.read(size)
            if not chunk:
                raise ConnectionError("Discord closed the connection")
            chunks.append(chunk)
            size -= len(chunk)
        return b"".join(chunks)

    def close(self):
        try:
            self.file.close()
        except OSError:
            pass


def _unix_candidate_dirs():
    bases = []
    for var in ("XDG_RUNTIME_DIR", "TMPDIR", "TMP", "TEMP"):
        value = os.environ.get(var)
        if value and value not in bases:
            bases.append(value)
    if "/tmp" not in bases:
        bases.append("/tmp")

    # Sandboxed clients put the socket in a subdirectory
    subdirs = ["", "app/com.discordapp.Discord", "app/com.discordapp.DiscordCanary",
               "app/com.discordapp.DiscordPTB", "snap.discord", "snap.discord-canary",
               ".flatpak/dev.vencord.Vesktop/xdg-run", "app/dev.vencord.Vesktop"]

    for base in bases:
        for sub in subdirs:
            yield os.path.join(base, sub) if sub else base


def ipc_paths(pipe: int):
    """Possible locations of Discord's IPC endpoint. ``pipe`` of -1 means "try 0 to 9"."""
    numbers = range(10) if pipe < 0 else [pipe]

    if sys.platform == "win32":
        for n in numbers:
            yield rf"\\.\pipe\discord-ipc-{n}"
        return

    for n in numbers:
        for directory in _unix_candidate_dirs():
            yield os.path.join(directory, f"discord-ipc-{n}")


def open_transport(pipe: int = -1):
    """Connects to the first Discord IPC endpoint that accepts a connection."""
    errors = []

    for path in ipc_paths(pipe):
        try:
            if sys.platform == "win32":
                transport = PipeTransport(path)
            else:
                if not os.path.exists(path):
                    continue
                sock = socket.socket(socket.AF_UNIX, socket.SOCK_STREAM)
                try:
                    sock.connect(path)
                except OSError:
                    sock.close()
                    raise
                transport = SocketTransport(sock)
        except OSError as e:
            errors.append(f"{path}: {e}")
            continue

        log.info("Connected to %s", path)
        return transport

    raise ConnectionError("Couldn't find a running Discord client" + (" (" + "; ".join(errors[-3:]) + ")" if errors else ""))


# --- Client --------------------------------------------------------------------

class DiscordIPC:
    """A Rich Presence connection to the Discord client.

    The connection runs on a background thread, and all the ``on_*`` callbacks are invoked
    from that thread:

    * ``on_ready(user)`` - handshake succeeded; ``user`` is Discord's user object
    * ``on_presence_update(activity)`` - Discord accepted a SET_ACTIVITY call
    * ``on_error(code, message)`` - Discord rejected something or closed the connection on purpose
    * ``on_connection_failed(message)`` - couldn't connect, or the connection dropped
    """

    def __init__(self, client_id: str, pipe: int = -1, *, on_ready=None, on_presence_update=None,
                 on_error=None, on_connection_failed=None, transport_factory=open_transport):
        self.client_id = client_id
        self.pipe = pipe
        self.user = None

        self.on_ready = on_ready or (lambda user: None)
        self.on_presence_update = on_presence_update or (lambda activity: None)
        self.on_error = on_error or (lambda code, message: None)
        self.on_connection_failed = on_connection_failed or (lambda message: None)

        self._transport_factory = transport_factory
        self._outgoing = queue.Queue()
        self._stop = threading.Event()
        self._thread = None

    # Public API

    def start(self):
        self._thread = threading.Thread(target=self._run, name="discord-ipc", daemon=True)
        self._thread.start()

    def set_activity(self, activity):
        """Queues a presence update; it's sent as soon as the handshake is done."""
        self._outgoing.put({
            "cmd": "SET_ACTIVITY",
            "args": {"pid": os.getpid(), "activity": activity},
            "nonce": str(uuid.uuid4()),
        })

    def close(self):
        """Stops the connection. Callbacks won't fire after this returns."""
        self._stop.set()
        if self._thread and self._thread is not threading.current_thread():
            self._thread.join(timeout=2)

    @property
    def closed(self) -> bool:
        return self._stop.is_set()

    # Worker thread

    def _run(self):
        transport = None
        try:
            transport = self._transport_factory(self.pipe)
            self._send(transport, OP_HANDSHAKE, {"v": 1, "client_id": self.client_id})

            while not self._stop.is_set():
                if self.user is not None:
                    self._flush(transport)

                if transport.poll(0.1):
                    if not self._handle(transport, *self._read(transport)):
                        break
        except (OSError, ConnectionError, ValueError) as e:
            if not self._stop.is_set():
                log.warning("Connection failed: %s", e)
                self.on_connection_failed(str(e))
        finally:
            if transport is not None:
                transport.close()
            log.info("Connection closed")

    def _flush(self, transport):
        while True:
            try:
                payload = self._outgoing.get_nowait()
            except queue.Empty:
                return
            self._send(transport, OP_FRAME, payload)

    def _send(self, transport, op, payload):
        data = json.dumps(payload, ensure_ascii=False).encode("utf-8")
        log.debug("-> op %d %s", op, data.decode("utf-8"))
        transport.send(HEADER.pack(op, len(data)) + data)

    def _read(self, transport):
        op, length = HEADER.unpack(transport.recv(HEADER.size))
        if not 0 <= length <= MAX_PAYLOAD:
            raise ValueError(f"Bad frame length {length}")
        data = transport.recv(length) if length else b"{}"
        log.debug("<- op %d %s", op, data.decode("utf-8", "replace"))
        return op, json.loads(data)

    def _handle(self, transport, op, payload) -> bool:
        """Processes a frame; returns False if the connection should end."""
        if self._stop.is_set():
            return False

        if op == OP_PING:
            self._send(transport, OP_PONG, payload)
            return True

        if op == OP_CLOSE:
            code = payload.get("code", 0)
            message = payload.get("message", "Connection closed by Discord")
            log.error("Closed by Discord: %s %s", code, message)
            self.on_error(code, message)
            return False

        if op != OP_FRAME:
            return True

        cmd = payload.get("cmd")
        evt = payload.get("evt")
        data = payload.get("data") or {}

        if evt == "ERROR":
            code = data.get("code", 0)
            message = data.get("message", "Unknown error")
            log.error("Error %s: %s", code, message)
            self.on_error(code, message)
        elif cmd == "DISPATCH" and evt == "READY":
            self.user = data.get("user") or {}
            self.on_ready(self.user)
        elif cmd == "SET_ACTIVITY":
            self.on_presence_update(data)

        return True


def avatar_url(user: dict, size: int = 128) -> str:
    if user.get("avatar"):
        return f"https://cdn.discordapp.com/avatars/{user['id']}/{user['avatar']}.png?size={size}"
    try:
        index = (int(user.get("id", 0)) >> 22) % 6
    except ValueError:
        index = 0
    return f"https://cdn.discordapp.com/embed/avatars/{index}.png"


def display_name(user: dict) -> str:
    name = user.get("username", "")
    discriminator = user.get("discriminator")
    if discriminator and discriminator != "0":
        return f"{name}#{discriminator}"
    return name
