"""Small Qt helpers shared by the windows."""

import threading
import urllib.request

from PySide6.QtCore import QObject, Qt, QUrl, Signal
from PySide6.QtGui import QDesktopServices, QGuiApplication
from PySide6.QtWidgets import QMessageBox

from .. import i18n

_pending = set()


class _Task(QObject):
    done = Signal(object, object)


def run_in_background(fn, callback):
    """Runs ``fn()`` on a worker thread, then ``callback(result, error)`` on the UI thread."""
    task = _Task()
    _pending.add(task)

    def finished(result, error):
        _pending.discard(task)
        callback(result, error)

    task.done.connect(finished, Qt.QueuedConnection)

    def work():
        try:
            result = fn()
        except Exception as e:  # Handed to the callback
            task.done.emit(None, e)
        else:
            task.done.emit(result, None)

    threading.Thread(target=work, daemon=True).start()


def http_get(url: str, timeout: float = 15) -> bytes:
    request = urllib.request.Request(url, headers={"User-Agent": "CustomRP"})
    with urllib.request.urlopen(request, timeout=timeout) as response:
        return response.read()


def open_url(url: str, parent=None):
    if QDesktopServices.openUrl(QUrl(url)):
        return
    QGuiApplication.clipboard().setText(url)
    error(parent, i18n.s("errorOpeningURL"))


def error(parent, text: str):
    QMessageBox.critical(parent, i18n.s("error"), text)


def info(parent, text: str):
    QMessageBox.information(parent, i18n.s("information"), text)


def ask(parent, title: str, text: str, default_yes: bool = True) -> bool:
    default = QMessageBox.Yes if default_yes else QMessageBox.No
    return QMessageBox.question(parent, title, text, QMessageBox.Yes | QMessageBox.No, default) == QMessageBox.Yes
