import json
import socket
import struct
import threading
from datetime import datetime, timedelta, timezone

import pytest

from customrp import discord_ipc, i18n, preset, presence, updates
from customrp.settings import CorruptSettingsError, Settings

# --- Settings -----------------------------------------------------------------


def test_settings_roundtrip(tmp_path):
    s = Settings.load(tmp_path / "s.json")
    s.details = "Hello"
    s.partySize = 3
    s.darkMode = False
    s.save()

    loaded = Settings.load(tmp_path / "s.json")
    assert (loaded.details, loaded.partySize, loaded.darkMode) == ("Hello", 3, False)


def test_settings_ignores_wrong_types(tmp_path):
    path = tmp_path / "s.json"
    path.write_text(json.dumps({"partySize": "lots", "darkMode": 1, "details": "ok"}))
    s = Settings.load(path)
    assert (s.partySize, s.darkMode, s.details) == (0, True, "ok")


def test_settings_corrupt(tmp_path):
    path = tmp_path / "s.json"
    path.write_text("{nope")
    with pytest.raises(CorruptSettingsError):
        Settings.load(path)


# --- Presence -----------------------------------------------------------------


def make_settings(**kwargs):
    s = Settings()
    for k, v in kwargs.items():
        setattr(s, k, v)
    return s


NOW = datetime(2026, 1, 2, 15, 30, 0).astimezone()


def build(settings):
    presence.normalize_settings(settings)
    return presence.build_activity(settings, connected_at=NOW - timedelta(hours=1),
                                   started_at=NOW - timedelta(hours=2), now=NOW)


def test_basic_activity():
    activity, refresh = build(make_settings(details="Coding", state="Rust", partySize=2, partyMax=4,
                                            button1Text="Site", button1URL="example.com"))
    assert activity["details"] == "Coding"
    assert activity["party"] == {"id": "CustomRP", "size": [2, 4]}
    assert activity["buttons"] == [{"label": "Site", "url": "https://example.com"}]
    assert activity["timestamps"] == {"start": int((NOW - timedelta(hours=1)).timestamp() * 1000)}
    assert refresh is None


def test_party_only_for_playing_and_max_fixed():
    s = make_settings(partySize=5, partyMax=2, type=2)
    activity, _ = build(s)
    assert s.partyMax == 5
    assert "party" not in activity


def test_competing_has_no_timestamps():
    activity, _ = build(make_settings(type=presence.COMPETING))
    assert "timestamps" not in activity


def test_local_time_refresh():
    activity, refresh = build(make_settings(timestamps=presence.TimestampType.LOCAL_TIME))
    midnight = NOW.replace(hour=0, minute=0, second=0)
    assert activity["timestamps"]["start"] == int(midnight.timestamp() * 1000)
    assert refresh == pytest.approx(8.5 * 3600 + 5)


def test_custom_future_timestamp_counts_down():
    s = make_settings(timestamps=presence.TimestampType.CUSTOM)
    s.set_datetime("customTimestamp", datetime(2026, 1, 3, 0, 0))
    activity, _ = build(s)
    assert activity["timestamps"]["start"] == int(NOW.timestamp() * 1000)
    assert activity["timestamps"]["end"] == int(datetime(2026, 1, 3).astimezone().timestamp() * 1000)


def test_leading_nbsp_gets_zwsp():
    s = make_settings(details=" indented")
    build(s)
    assert s.details == "​ indented"


def test_url_processing():
    assert presence.process_url("bücher.de/a b", 256) == "https://xn--bcher-kva.de/a%20b"
    assert presence.process_url("https://x.com/" + "a" * 300, 256) == ("https://x.com/" + "a" * 300)[:256]


def test_discord_cdn_images_are_proxied():
    activity, _ = build(make_settings(largeKey="https://cdn.discordapp.com/attachments/1/2/a.png"))
    assert activity["assets"]["large_image"] == "https://customrp.xyz/proxy/attachments/1/2/a.png"


def test_long_image_url_rejected():
    with pytest.raises(presence.PresenceError) as e:
        build(make_settings(smallKey="https://example.com/" + "a" * 250))
    assert e.value.field == "labelSmall"


def test_field_validation():
    assert presence.is_field_invalid("details", "a")
    assert not presence.is_field_invalid("details", "ab")
    assert presence.is_field_invalid("button1Text", "é" * 16)  # 32 bytes


# --- Presets ------------------------------------------------------------------

# Written by the Windows app's XmlSerializer
WINDOWS_PRESET = """<?xml version="1.0"?>
<Preset xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ID>1234567890</ID>
  <Type>2</Type>
  <Display>0</Display>
  <Name />
  <Details>Listening</Details>
  <State>to stuff</State>
  <PartySize>0</PartySize>
  <PartyMax>0</PartyMax>
  <Timestamps>3</Timestamps>
  <CustomTimestamp>2024-05-01T12:00:00.1234567+00:00</CustomTimestamp>
  <CustomTimestampEndEnabled>true</CustomTimestampEndEnabled>
  <CustomTimestampEnd>0001-01-01T00:00:00</CustomTimestampEnd>
  <LargeKey>logo</LargeKey>
  <Button1Text>Site</Button1Text>
  <Button1URL>https://example.com/</Button1URL>
</Preset>
"""


def test_load_windows_preset(tmp_path):
    path = tmp_path / "p.crp"
    path.write_text(WINDOWS_PRESET, encoding="utf-8")
    s = Settings()
    s.smallKey = "leftover"
    preset.load_preset(path, s)

    assert (s.id, s.type, s.details, s.largeKey, s.smallKey) == ("1234567890", 2, "Listening", "logo", "")
    assert s.customTimestampEndEnabled is True
    assert s.customTimestampEnd == ""
    start = datetime.fromisoformat(s.customTimestamp).astimezone()
    assert start == datetime(2024, 5, 1, 12, 0, tzinfo=timezone.utc)


def test_preset_roundtrip(tmp_path):
    path = tmp_path / "p.crp"
    original = make_settings(id="42", details="d", partySize=1, partyMax=3, customTimestampEndEnabled=True,
                             customTimestamp="2025-06-07T08:09:10")
    preset.save_preset(path, original)
    loaded = Settings()
    preset.load_preset(path, loaded)
    for _, field, _ in preset.FIELDS:
        if field != "customTimestampEnd":
            assert getattr(loaded, field) == getattr(original, field), field


def test_bad_preset(tmp_path):
    path = tmp_path / "p.crp"
    path.write_text("<NotAPreset/>")
    with pytest.raises(preset.PresetError):
        preset.load_preset(path, Settings())


# --- i18n / updates -------------------------------------------------------------


def test_language_resolution():
    assert i18n.resolve("auto", ["de-AT"]) == "de"
    assert i18n.resolve("auto", ["pt-BR"]) == "pt-br"
    assert i18n.resolve("auto", ["zh-TW"]) == "zh-hant"
    assert i18n.resolve("auto", ["zh-Hans-CN"]) == "zh-hans"
    assert i18n.resolve("auto", ["xx"]) == "en"
    assert i18n.resolve("ru", ["de"]) == "ru"


def test_translations_fall_back():
    i18n.load("de-ch")
    assert i18n.s("error")
    assert i18n.form("labelID.Text") == "ID"
    i18n.load("ar")
    assert i18n.is_rtl()
    i18n.load("en")


def test_versions():
    assert updates.parse_version("v1.19") == (1, 19, 0, 0)
    assert updates.version_string((1, 19, 4, 1)) == "1.19.4.1"
    assert updates.version_string((2, 3, 0, 0)) == "2.3"
    with pytest.raises(ValueError):
        updates.parse_version("1")


# --- IPC ----------------------------------------------------------------------


def frame(op, payload):
    data = json.dumps(payload).encode()
    return struct.pack("<ii", op, len(data)) + data


def recv_exact(sock, size):
    data = b""
    while len(data) < size:
        chunk = sock.recv(size - len(data))
        assert chunk, "client hung up"
        data += chunk
    return data


def read_frame(sock):
    op, length = struct.unpack("<ii", recv_exact(sock, 8))
    return op, json.loads(recv_exact(sock, length))


def run_client(fake_discord, activity=None):
    """Runs a DiscordIPC client against a fake Discord driven by ``fake_discord(sock)``."""
    client_sock, server_sock = socket.socketpair()
    events = []
    done = threading.Event()

    client = discord_ipc.DiscordIPC(
        "123", transport_factory=lambda pipe: discord_ipc.SocketTransport(client_sock),
        on_ready=lambda user: events.append(("ready", user["username"])),
        on_presence_update=lambda data: (events.append(("presence", data)), done.set()),
        on_error=lambda code, msg: (events.append(("error", code, msg)), done.set()),
        on_connection_failed=lambda msg: (events.append(("failed",)), done.set()),
    )
    if activity is not None:
        client.set_activity(activity)  # Queued until READY

    server = threading.Thread(target=fake_discord, args=(server_sock,))
    server.start()
    client.start()
    assert done.wait(5)
    client.close()
    server.join(5)
    server_sock.close()
    return events


def test_ipc_handshake_and_activity():
    received = {}

    def fake_discord(sock):
        received["handshake"] = read_frame(sock)
        sock.sendall(frame(discord_ipc.OP_PING, {"x": 1}))
        received["pong"] = read_frame(sock)
        sock.sendall(frame(1, {"cmd": "DISPATCH", "evt": "READY", "data": {"user": {"username": "tester"}}}))
        _, cmd = read_frame(sock)
        received["cmd"] = cmd
        sock.sendall(frame(1, {"cmd": "SET_ACTIVITY", "evt": None, "data": cmd["args"]["activity"],
                               "nonce": cmd["nonce"]}))

    events = run_client(fake_discord, activity={"details": "hi"})

    assert received["pong"] == (discord_ipc.OP_PONG, {"x": 1})
    assert received["handshake"] == (0, {"v": 1, "client_id": "123"})
    assert received["cmd"]["cmd"] == "SET_ACTIVITY"
    assert events == [("ready", "tester"), ("presence", {"details": "hi"})]


def test_ipc_invalid_client_id():
    def fake_discord(sock):
        read_frame(sock)
        sock.sendall(frame(discord_ipc.OP_CLOSE, {"code": 4000, "message": "Invalid Client ID"}))

    events = run_client(fake_discord)
    assert events == [("error", 4000, "Invalid Client ID")]


def test_ipc_connection_failed():
    def failing(pipe):
        raise ConnectionError("no discord")

    done = threading.Event()
    client = discord_ipc.DiscordIPC("1", transport_factory=failing, on_connection_failed=lambda m: done.set())
    client.start()
    assert done.wait(5)


def test_display_name():
    assert discord_ipc.display_name({"username": "a", "discriminator": "0"}) == "a"
    assert discord_ipc.display_name({"username": "a", "discriminator": "1234"}) == "a#1234"
