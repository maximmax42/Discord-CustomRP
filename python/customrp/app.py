"""Application startup: single instance handling, settings, translations and the main window."""

import getpass
import json
import logging
import sys
import traceback
from datetime import datetime, timezone

from PySide6.QtCore import QByteArray, QLocale, Qt
from PySide6.QtNetwork import QLocalServer, QLocalSocket
from PySide6.QtWidgets import QApplication, QMessageBox

from . import APP_NAME, __version__, i18n
from .paths import crash_log, settings_file
from .settings import CorruptSettingsError, Settings
from .ui import theme

log = logging.getLogger("customrp")


def _server_name(second_instance: bool) -> str:
    name = f"CustomRP-{getpass.getuser()}"
    return name + " 2" if second_instance else name


def notify_running_instance(second_instance: bool, message: dict) -> bool:
    """Passes a message to an already running instance. Returns False if there's none."""
    socket = QLocalSocket()
    socket.connectToServer(_server_name(second_instance))
    if not socket.waitForConnected(500):
        return False
    socket.write(QByteArray(json.dumps(message).encode("utf-8") + b"\n"))
    socket.waitForBytesWritten(1000)
    socket.disconnectFromServer()
    return True


class CustomRPApp:
    def __init__(self, qt_app: QApplication, second_instance: bool):
        self.qt_app = qt_app
        self.second_instance = second_instance
        self.started_at = datetime.now(timezone.utc)
        self.window = None
        self.server = None
        self.settings = None

    def load_settings(self) -> bool:
        path = settings_file(self.second_instance)
        try:
            self.settings = Settings.load(path)
            return True
        except CorruptSettingsError as e:
            log.error("Corrupt settings: %s", e)
            answer = QMessageBox.critical(None, i18n.s("error"), i18n.s("errorCorruptSettings"),
                                          QMessageBox.Yes | QMessageBox.No)
            if answer != QMessageBox.Yes:
                return False
            path.unlink(missing_ok=True)
            self.settings = Settings.load(path)
            return True

    def apply_language(self):
        i18n.load(i18n.resolve(self.settings.language, QLocale.system().uiLanguages()))
        self.qt_app.setLayoutDirection(Qt.RightToLeft if i18n.is_rtl() else Qt.LeftToRight)

    def listen(self):
        """Starts accepting messages from instances launched later."""
        name = _server_name(self.second_instance)
        QLocalServer.removeServer(name)  # Left behind if a previous run crashed
        self.server = QLocalServer()
        self.server.newConnection.connect(self._new_connection)
        if not self.server.listen(name):
            log.warning("Couldn't listen for other instances: %s", self.server.errorString())

    def _new_connection(self):
        socket = self.server.nextPendingConnection()
        buffer = bytearray()

        def read():
            buffer.extend(bytes(socket.readAll()))
            if b"\n" not in buffer:
                return
            try:
                message = json.loads(buffer.split(b"\n", 1)[0])
            except ValueError:
                return
            finally:
                socket.disconnectFromServer()
            self._handle_message(message)

        socket.readyRead.connect(read)
        socket.disconnected.connect(socket.deleteLater)

    def _handle_message(self, message: dict):
        if not self.window:
            return
        if message.get("show"):
            self.window.maximize_from_tray()
        if message.get("preset"):
            self.window.load_preset_file(message["preset"])

    def show_window(self, preset_file=None, language_changed=False, was_connected=False):
        from .ui.main_window import MainWindow
        self.window = MainWindow(self, preset_file, language_changed, was_connected)

    def change_language(self, code: str, was_connected: bool):
        """Rebuilds the window in the new language, reconnecting if it was connected."""
        self.settings.language = code
        self.settings.save()

        old = self.window
        old.shutdown()
        old.deleteLater()

        self.apply_language()
        self.show_window(language_changed=True, was_connected=was_connected)


def _install_crash_handler():
    def handler(exc_type, exc, tb):
        text = "".join(traceback.format_exception(exc_type, exc, tb))
        log.critical(text)
        try:
            crash_log().parent.mkdir(parents=True, exist_ok=True)
            with open(crash_log(), "a", encoding="utf-8") as f:
                f.write(f"{datetime.now()}\n{text}\n")
        except OSError:
            pass
        if QApplication.instance():
            QMessageBox.critical(None, f"CustomRP - {i18n.s('error')}", text)
        else:
            sys.__excepthook__(exc_type, exc, tb)

    sys.excepthook = handler


def run(preset_file=None, second_instance=False, silent=False) -> int:
    qt_app = QApplication(sys.argv)
    qt_app.setApplicationName(APP_NAME)
    qt_app.setApplicationVersion(__version__)
    qt_app.setDesktopFileName("customrp")
    qt_app.setQuitOnLastWindowClosed(False)  # Lives on in the tray
    qt_app.setWindowIcon(theme.icon("favicon.ico"))

    i18n.load("en")  # For errors shown before settings are loaded

    # Hand the request to the instance that's already running, if any
    if notify_running_instance(second_instance, {"show": not silent, "preset": preset_file}):
        return 0

    _install_crash_handler()

    app = CustomRPApp(qt_app, second_instance)
    if not app.load_settings():
        return 1

    app.apply_language()
    theme.apply(qt_app, app.settings.darkMode)
    app.listen()
    app.show_window(preset_file)

    log.info("CustomRP %s started", __version__)
    return qt_app.exec()
