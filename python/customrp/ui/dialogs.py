"""About, Update Available and Pipe dialogs."""

from PySide6.QtCore import Qt, Signal
from PySide6.QtGui import QPixmap
from PySide6.QtWidgets import (QCheckBox, QDialog, QDialogButtonBox, QGridLayout, QHBoxLayout, QLabel,
                               QPushButton, QSpinBox, QTextBrowser, QVBoxLayout)

from .. import __version__, i18n
from ..discord_ipc import DiscordIPC, avatar_url, display_name
from ..presence import DEFAULT_ID
from ..updates import parse_version, version_string
from . import theme
from .util import http_get, open_url, run_in_background


def _logo(size: int) -> QLabel:
    label = QLabel()
    label.setPixmap(QPixmap(str(theme.ASSETS_DIR / "logo.png")).scaled(
        size, size, Qt.KeepAspectRatio, Qt.SmoothTransformation))
    return label


class AboutDialog(QDialog):
    def __init__(self, parent=None):
        super().__init__(parent)
        tr = lambda key: i18n.tr("About." + key)
        self.setWindowTitle(tr("$this.Text"))

        title = QLabel(tr("labelTitle.Text"))
        font = title.font()
        font.setPointSizeF(font.pointSizeF() * 1.4)
        font.setBold(True)
        title.setFont(font)

        version = QLabel(version_string(parse_version(__version__)))
        website = QLabel(f'<a href="https://www.customrp.xyz">{tr("linkLabelWebsite.Text")}</a>')
        website.linkActivated.connect(lambda url: open_url(url, self))

        close = QPushButton(tr("buttonClose.Text"))
        close.clicked.connect(self.accept)

        text = QVBoxLayout()
        text.addWidget(title)
        text.addWidget(QLabel(tr("labelMadeBy.Text")))
        text.addWidget(version)
        text.addWidget(website)
        text.addStretch()

        top = QHBoxLayout()
        top.addWidget(_logo(96), 0, Qt.AlignTop)
        top.addLayout(text)

        layout = QVBoxLayout(self)
        layout.addLayout(top)
        layout.addWidget(close, 0, Qt.AlignRight)


class UpdateDialog(QDialog):
    """Asks whether to download an update. Result: Accepted (download), Rejected (not now), or SKIP."""

    SKIP = 2

    def __init__(self, info, settings, parent=None):
        super().__init__(parent)
        tr = lambda key: i18n.tr("UpdatePrompt." + key)
        self.settings = settings
        self.setWindowTitle(tr("$this.Text"))
        self.resize(520, 440)

        question = QLabel(tr("labelQuestion.Text"))
        font = question.font()
        font.setBold(True)
        question.setFont(font)

        versions = QGridLayout()
        for row, (caption, value) in enumerate(zip(tr("labelVersionsText.Text").split("\n"),
                                                   (info.current, info.latest))):
            versions.addWidget(QLabel(caption), row, 0)
            versions.addWidget(QLabel(version_string(value)), row, 1)
        versions.setColumnStretch(2, 1)

        changelog = QTextBrowser()
        changelog.setOpenLinks(False)
        changelog.anchorClicked.connect(lambda url: open_url(url.toString(), self))
        changelog.setMarkdown(info.changelog)

        never = QCheckBox(tr("checkBoxNeverNotify.Text"))
        never.toggled.connect(self._toggle_updates)

        buttons = QDialogButtonBox()
        update = buttons.addButton(tr("buttonUpdate.Text"), QDialogButtonBox.AcceptRole)
        buttons.addButton(tr("buttonNotNow.Text"), QDialogButtonBox.RejectRole)
        skip = buttons.addButton(tr("buttonSkipUpdate.Text"), QDialogButtonBox.DestructiveRole)
        update.setDefault(True)
        buttons.accepted.connect(self.accept)
        buttons.rejected.connect(self.reject)
        skip.clicked.connect(lambda: self.done(self.SKIP))

        header = QVBoxLayout()
        header.addWidget(question)
        header.addLayout(versions)

        top = QHBoxLayout()
        top.addWidget(_logo(64))
        top.addLayout(header, 1)

        layout = QVBoxLayout(self)
        layout.addLayout(top)
        layout.addWidget(changelog, 1)
        layout.addWidget(never)
        layout.addWidget(buttons)

    def _toggle_updates(self, checked):
        self.settings.checkUpdates = not checked
        self.settings.save()


class PipeDialog(QDialog):
    """Lets the user pick which Discord IPC pipe to use and shows who's logged in on it."""

    _result = Signal(object, str, str)  # client, username, avatar URL

    def __init__(self, settings, parent=None):
        super().__init__(parent)
        tr = lambda key: i18n.tr("PipeSelector." + key)
        self.settings = settings
        self.client = None
        self.setWindowTitle(tr("$this.Text"))

        self.spin = QSpinBox()
        self.spin.setRange(-1, 9)
        self.spin.setValue(settings.pipe)
        self.spin.valueChanged.connect(self._pipe_changed)

        self.avatar = QLabel()
        self.avatar.setFixedSize(64, 64)
        self.username = QLabel()

        ok = QPushButton(tr("buttonOK.Text"))
        ok.clicked.connect(self.accept)

        grid = QGridLayout(self)
        grid.addWidget(QLabel(tr("labelPipe.Text")), 0, 0)
        grid.addWidget(self.spin, 0, 1)
        grid.addWidget(QLabel(tr("labelDefault.Text")), 1, 0, 1, 2)
        grid.addWidget(self.avatar, 0, 2, 2, 1)
        grid.addWidget(self.username, 0, 3, 2, 1)
        grid.addWidget(ok, 2, 0, 1, 4, Qt.AlignRight)

        self._result.connect(self._show_result)
        self._test()

    def _pipe_changed(self, value):
        self.settings.pipe = value
        self.settings.save()
        self._test()

    def _test(self):
        if self.client:
            self.client.close()

        self.username.setText(i18n.tr("PipeSelector.labelUsername.Text"))
        self._load_avatar("https://cdn.discordapp.com/embed/avatars/1.png")

        client = DiscordIPC(DEFAULT_ID, self.spin.value())
        client.on_ready = lambda user: self._result.emit(client, display_name(user).replace("#", "\n#"), avatar_url(user))
        client.on_connection_failed = lambda message: self._result.emit(
            client, "Can't connect.", "https://cdn.discordapp.com/embed/avatars/4.png")
        client.on_error = lambda code, message: self._result.emit(
            client, message, "https://cdn.discordapp.com/embed/avatars/4.png")
        self.client = client
        client.start()

    def _show_result(self, client, username, avatar):
        if client is not self.client:
            return
        client.close()
        self.username.setText(username)
        self._load_avatar(avatar)

    def _load_avatar(self, url):
        def show(data, error):
            if error:
                return
            pixmap = QPixmap()
            if pixmap.loadFromData(data):
                self.avatar.setPixmap(pixmap.scaled(64, 64, Qt.KeepAspectRatio, Qt.SmoothTransformation))

        run_in_background(lambda: http_get(url), show)

    def done(self, result):
        if self.client:
            self.client.close()
        super().done(result)
