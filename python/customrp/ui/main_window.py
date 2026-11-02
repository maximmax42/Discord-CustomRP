"""The main window, tray icon and connection handling (the port of MainForm.cs)."""

import enum
import json
import logging
import re
from datetime import datetime, timedelta, timezone

from PySide6.QtCore import QDateTime, QEvent, QLocale, QObject, Qt, QTimer, Signal
from PySide6.QtGui import QAction, QActionGroup, QKeySequence
from PySide6.QtWidgets import (QApplication, QButtonGroup, QCheckBox, QComboBox, QDateTimeEdit, QFileDialog, QFrame,
                               QGridLayout, QHBoxLayout, QLabel, QLineEdit, QMainWindow, QMenu, QMessageBox,
                               QPushButton, QRadioButton, QSizePolicy, QSpinBox, QSystemTrayIcon, QVBoxLayout,
                               QWidget)

from .. import __version__, autostart, i18n
from ..discord_ipc import DiscordIPC, display_name
from ..i18n import form, s
from ..presence import (ACTIVITY_TYPES, COMPETING, DEFAULT_ID, DISPLAY_TYPES, MAX_LENGTH, NBSP, PLAYING,
                        PresenceError, TimestampType, build_activity, is_field_invalid, normalize_settings)
from ..preset import PresetError, load_preset, save_preset
from ..updates import RELEASES_PAGE, check as check_updates, parse_version, version_string
from . import theme
from .dialogs import AboutDialog, PipeDialog, UpdateDialog
from .util import ask, error, http_get, info, open_url, run_in_background

log = logging.getLogger("customrp.ui")

RESTART_INTERVAL_MS = 10 * 1000
RESTART_ATTEMPTS = 30
PRESET_FILTER = "CustomRP Preset (*.crp)"

# Field name -> resource name of its label (for the tooltip and error messages)
TEXT_FIELDS = {
    "id": "labelID",
    "name": "labelName",
    "details": "labelDetails",
    "detailsURL": "labelDetailsURL",
    "state": "labelState",
    "stateURL": "labelStateURL",
    "largeKey": "labelLargeKey",
    "largeText": "labelLargeText",
    "largeURL": "labelLargeURL",
    "smallKey": "labelSmallKey",
    "smallText": "labelSmallText",
    "smallURL": "labelSmallURL",
    "button1Text": "labelButton1Text",
    "button1URL": "labelButton1URL",
    "button2Text": "labelButton2Text",
    "button2URL": "labelButton2URL",
}

# Earliest and latest timestamps Discord displays properly (1 and 99999999999 in unix seconds)
MIN_TIMESTAMP = QDateTime.fromSecsSinceEpoch(1)
MAX_TIMESTAMP = QDateTime.fromSecsSinceEpoch(99999999999)


class State(enum.Enum):
    DISCONNECTED = enum.auto()
    CONNECTING = enum.auto()
    UPDATING = enum.auto()
    CONNECTED = enum.auto()
    ERROR = enum.auto()


class _Bridge(QObject):
    """Carries callbacks from the IPC thread to the UI thread. Each signal carries the client it came from."""

    ready = Signal(object, dict)
    presence_updated = Signal(object, dict)
    error = Signal(object, int, str)
    connection_failed = Signal(object, str)


class AssetComboBox(QComboBox):
    """An editable combo box that asks for the app's assets before dropping down."""

    about_to_popup = Signal(object)

    def __init__(self):
        super().__init__()
        self.setEditable(True)
        self.setInsertPolicy(QComboBox.NoInsert)

    def showPopup(self):
        self.about_to_popup.emit(self)
        super().showPopup()


def _separator():
    line = QFrame()
    line.setObjectName("separator")
    line.setFrameShape(QFrame.HLine)
    return line


class MainWindow(QMainWindow):
    def __init__(self, app, preset_file=None, language_changed=False, was_connected=False):
        super().__init__()
        self.app = app
        self.settings = app.settings

        self.client = None
        self.state = State.DISCONNECTED
        self.retrying = False
        self.restart_attempts_left = -1
        self.connected_at = datetime.now(timezone.utc)
        self.started_at = app.started_at
        self.latest_update = None
        self.last_assets_id = None
        self.next_assets_check = datetime.min
        self.tooltip_shown = False
        self.quitting = False
        self.loading = True

        self.bridge = _Bridge()
        self.bridge.ready.connect(self._on_ready)
        self.bridge.presence_updated.connect(self._on_presence_updated)
        self.bridge.error.connect(self._on_error)
        self.bridge.connection_failed.connect(self._on_connection_failed)

        self.restart_timer = QTimer(self, singleShot=True, interval=RESTART_INTERVAL_MS)
        self.restart_timer.timeout.connect(self._restart_elapsed)
        self.local_time_timer = QTimer(self, singleShot=True)
        self.local_time_timer.timeout.connect(self.set_presence)
        self.save_timer = QTimer(self, singleShot=True, interval=1000)
        self.save_timer.timeout.connect(self.save_settings)

        self.title = "CustomRP " + version_string(parse_version(__version__))
        if app.second_instance:
            self.title += " (2)"
        self.setWindowTitle(self.title)
        self.setWindowIcon(theme.icon("favicon.ico"))
        self.setAcceptDrops(True)

        self._build_ui()
        self._build_menus()
        self._build_tray()
        self._load_fields()
        self._sync_autostart()

        if preset_file:
            self.load_preset_file(preset_file)

        self.loading = False
        self._set_state(State.DISCONNECTED)

        if language_changed or not self.settings.startMinimized or not self.tray:
            self.show()

        if self.settings.firstStart:
            self.settings.firstStart = False
            self.save_settings()
            if ask(self, s("firstTimeRun"), s("firstTimeRunText")):
                open_url(self._docs_url("https://docs.customrp.xyz/setting-up"), self)

        if (language_changed and was_connected) or (self.settings.autoconnect and not language_changed):
            self.connect_rpc()

        if self.settings.checkUpdates and not language_changed:
            self.check_for_updates(manual=False)

    # --- UI construction ---------------------------------------------------------

    def _label(self, name):
        label = QLabel(form(name + ".Text"))
        tooltip = i18n.tr(f"MainForm.{name}.ToolTip", "")
        if tooltip:
            label.setToolTip(tooltip)
        return label

    def _line_edit(self, field):
        edit = QLineEdit()
        edit.setMaxLength(MAX_LENGTH[field])
        return edit

    def _build_ui(self):
        self.fields = {}

        for field in TEXT_FIELDS:
            if field in ("largeKey", "smallKey"):
                combo = AssetComboBox()
                combo.lineEdit().setMaxLength(MAX_LENGTH[field])
                combo.about_to_popup.connect(self._fetch_assets)
                self.fields[field] = combo
            else:
                self.fields[field] = self._line_edit(field)

        for field, widget in self.fields.items():
            edit = self._edit(field)
            edit.textChanged.connect(lambda text, f=field: self._field_changed(f, text))
            if field != "id":
                edit.installEventFilter(self)

        self.fields["id"].textEdited.connect(self._only_numbers)
        self.fields["id"].setFixedWidth(self.fields["id"].fontMetrics().horizontalAdvance("0" * 22))

        # Their change signals get connected at the end, once the panels they toggle exist
        self.type_combo = QComboBox()
        for key, value in ACTIVITY_TYPES:
            self.type_combo.addItem(s(key), value)

        self.display_combo = QComboBox()
        for name, value in DISPLAY_TYPES:
            self.display_combo.addItem(name, value)

        self.party_size = QSpinBox()
        self.party_max = QSpinBox()
        for spin in (self.party_size, self.party_max):
            spin.setRange(0, 2147483647)
            spin.setMinimumWidth(spin.fontMetrics().horizontalAdvance("0" * 8))
            spin.valueChanged.connect(self._party_changed)
        self.party_size.editingFinished.connect(lambda: self._validate_party(self.party_size))
        self.party_max.editingFinished.connect(lambda: self._validate_party(self.party_max))

        self.timestamp_group = QButtonGroup(self)
        radios = QVBoxLayout()
        for name, kind in (("radioButtonLastConnection", TimestampType.SINCE_LAST_CONNECTION),
                           ("radioButtonStartTime", TimestampType.SINCE_STARTUP),
                           ("radioButtonPresence", TimestampType.SINCE_PRESENCE_UPDATE),
                           ("radioButtonLocalTime", TimestampType.LOCAL_TIME),
                           ("radioButtonCustom", TimestampType.CUSTOM)):
            radio = QRadioButton(form(name + ".Text"))
            radio.setToolTip(i18n.tr(f"MainForm.{name}.ToolTip", ""))
            self.timestamp_group.addButton(radio, kind)
            radios.addWidget(radio)
        self.timestamp_group.idToggled.connect(self._timestamp_changed)

        # Short date and long time like the Windows app, minus the time zone Qt's long format adds
        locale = QLocale()
        date_format = locale.dateFormat(QLocale.ShortFormat) + " " + \
            re.sub(r"\s*t+", "", locale.timeFormat(QLocale.LongFormat)).strip()
        self.start_dt = QDateTimeEdit(calendarPopup=True)
        self.end_dt = QDateTimeEdit(calendarPopup=True)
        for dt in (self.start_dt, self.end_dt):
            dt.setDisplayFormat(date_format)
            dt.setDateTimeRange(MIN_TIMESTAMP.toLocalTime(), MAX_TIMESTAMP.toLocalTime())
            dt.dateTimeChanged.connect(self._custom_timestamp_changed)
        self.end_check = QCheckBox(form("checkBoxTimestampEnd.Text"))
        self.end_check.toggled.connect(self._end_toggled)

        self.connect_button = QPushButton(form("buttonConnect.Text"))
        self.connect_button.setToolTip(form("buttonConnect.ToolTip"))
        self.connect_button.clicked.connect(self._connect_clicked)
        self.disconnect_button = QPushButton(form("buttonDisconnect.Text"))
        self.disconnect_button.setToolTip(form("buttonDisconnect.ToolTip"))
        self.disconnect_button.clicked.connect(self.disconnect_rpc)
        self.update_button = QPushButton(form("buttonUpdatePresence.Text"))
        self.update_button.setToolTip(form("buttonUpdatePresence.ToolTip"))
        self.update_button.clicked.connect(self._update_clicked)

        # Layout

        top = QHBoxLayout()
        top.addWidget(self._label("labelID"))
        top.addWidget(self.fields["id"])
        top.addSpacing(12)
        top.addWidget(self._label("labelType"))
        top.addWidget(self.type_combo, 1)
        top.addSpacing(12)
        top.addWidget(self._label("labelDisplay"))
        top.addWidget(self.display_combo, 1)

        text = QGridLayout()
        text.setColumnStretch(1, 3)
        text.setColumnStretch(3, 2)
        text.addWidget(self._label("labelName"), 0, 0)
        text.addWidget(self.fields["name"], 0, 1, 1, 3)
        for row, field in ((1, "details"), (2, "state")):
            text.addWidget(self._label(TEXT_FIELDS[field]), row, 0)
            text.addWidget(self.fields[field], row, 1)
            text.addWidget(self._label(TEXT_FIELDS[field + "URL"]), row, 2)
            text.addWidget(self.fields[field + "URL"], row, 3)

        self.party_panel = QWidget()
        party = QHBoxLayout(self.party_panel)
        party.setContentsMargins(0, 0, 0, 0)
        party.addWidget(self.party_size)
        party.addWidget(QLabel(form("labelPartyOf.Text")))
        party.addWidget(self.party_max)
        party.addStretch()
        text.addWidget(self._label("labelParty"), 3, 0)
        text.addWidget(self.party_panel, 3, 1, 1, 3)

        self.custom_panel = QWidget()
        custom = QGridLayout(self.custom_panel)
        custom.setContentsMargins(0, 0, 0, 0)
        custom.addWidget(QLabel(form("labelTimestampStart.Text")), 0, 0)
        custom.addWidget(self.start_dt, 0, 1)
        custom.addWidget(self.end_check, 1, 0)
        custom.addWidget(self.end_dt, 1, 1)
        custom.setColumnStretch(1, 1)
        custom.setRowStretch(2, 1)

        self.timestamp_panel = QWidget()
        timestamps = QHBoxLayout(self.timestamp_panel)
        timestamps.setContentsMargins(12, 0, 0, 0)
        timestamps.addLayout(radios)
        timestamps.addSpacing(12)
        timestamps.addWidget(self.custom_panel, 1)

        images = QGridLayout()
        images.setColumnStretch(1, 1)
        images.setColumnStretch(3, 1)
        for col, prefix in ((0, "large"), (2, "small")):
            Prefix = prefix.capitalize()
            heading = self._label("label" + Prefix)
            images.addWidget(heading, 0, col, 1, 2, Qt.AlignHCenter)
            for row, suffix in ((1, "Key"), (2, "Text"), (3, "URL")):
                images.addWidget(self._label(TEXT_FIELDS[prefix + suffix]), row, col)
                images.addWidget(self.fields[prefix + suffix], row, col + 1)

        buttons = QGridLayout()
        buttons.setColumnStretch(1, 1)
        buttons.setColumnStretch(3, 1)
        for col, n in ((0, 1), (2, 2)):
            buttons.addWidget(self._label(f"labelButton{n}"), 0, col, 1, 2, Qt.AlignHCenter)
            for row, suffix in ((1, "Text"), (2, "URL")):
                buttons.addWidget(self._label(TEXT_FIELDS[f"button{n}{suffix}"]), row, col)
                buttons.addWidget(self.fields[f"button{n}{suffix}"], row, col + 1)

        actions = QHBoxLayout()
        actions.addWidget(self.connect_button)
        actions.addWidget(self.disconnect_button)
        actions.addStretch()
        actions.addWidget(self.update_button)

        layout = QVBoxLayout()
        layout.addLayout(top)
        layout.addWidget(_separator())
        layout.addLayout(text)
        layout.addWidget(_separator())
        layout.addWidget(self._label("labelTimestamp"))
        layout.addWidget(self.timestamp_panel)
        layout.addWidget(_separator())
        layout.addLayout(images)
        layout.addWidget(_separator())
        layout.addLayout(buttons)
        layout.addLayout(actions)

        central = QWidget()
        central.setLayout(layout)
        self.setCentralWidget(central)

        self.username_label = QLabel()
        self.status_label = QLabel()
        self.statusBar().addWidget(self.username_label, 1)
        self.statusBar().addPermanentWidget(self.status_label)
        self.statusBar().setSizeGripEnabled(False)

        self.setMinimumWidth(640)

        self.type_combo.currentIndexChanged.connect(self._type_changed)
        self.display_combo.currentIndexChanged.connect(self._display_changed)

    def _edit(self, field) -> QLineEdit:
        widget = self.fields[field]
        return widget.lineEdit() if isinstance(widget, QComboBox) else widget

    def _menu_action(self, menu, key, slot=None, shortcut=None, checkable=False, checked=False):
        action = QAction(form(key + ".Text"), self)
        if shortcut:
            action.setShortcut(QKeySequence(shortcut))
        if checkable:
            action.setCheckable(True)
            action.setChecked(checked)
        if slot:
            action.triggered.connect(slot)
        menu.addAction(action)
        return action

    def _build_menus(self):
        bar = self.menuBar()
        settings = self.settings

        file = bar.addMenu(form("fileToolStripMenuItem.Text"))
        self._menu_action(file, "newPresetToolStripMenuItem", self.new_preset, "Ctrl+N")
        self._menu_action(file, "loadPresetToolStripMenuItem", self.load_preset_dialog, "Ctrl+O")
        self._menu_action(file, "savePresetToolStripMenuItem", self.save_preset_dialog, "Ctrl+S")
        self._menu_action(file, "uploadAssetsToolStripMenuItem", self.open_assets_page, "Ctrl+U")
        file.addSeparator()
        self._menu_action(file, "quitToolStripMenuItem", self.quit, "Ctrl+Q")

        menu = bar.addMenu(form("settingsToolStripMenuItem.Text"))
        menu.setToolTipsVisible(True)
        self.startup_action = self._menu_action(menu, "runOnStartupToolStripMenuItem", self._toggle_startup,
                                                checkable=True, checked=settings.runOnStartup)
        self._menu_action(menu, "startMinimizedToolStripMenuItem", lambda c: self._toggle("startMinimized", c),
                          checkable=True, checked=settings.startMinimized)
        self._menu_action(menu, "autoconnectToolStripMenuItem", lambda c: self._toggle("autoconnect", c),
                          checkable=True, checked=settings.autoconnect)
        self.check_updates_action = self._menu_action(menu, "checkUpdatesToolStripMenuItem", self._toggle_check_updates,
                                                      checkable=True, checked=settings.checkUpdates)
        self._menu_action(menu, "darkModeToolStripMenuItem", self._toggle_dark_mode,
                          checkable=True, checked=settings.darkMode)
        self._build_language_menu(menu.addMenu(form("languageToolStripMenuItem.Text")))

        help_menu = bar.addMenu(form("helpToolStripMenuItem.Text"))
        help_menu.setToolTipsVisible(True)
        self._menu_action(help_menu, "openTheManualToolStripMenuItem",
                          lambda: open_url(self._docs_url("https://docs.customrp.xyz/setting-up"), self), "F1")
        self._menu_action(help_menu, "faqToolStripMenuItem",
                          lambda: open_url(self._docs_url("https://docs.customrp.xyz/faq"), self))
        self._menu_action(help_menu, "gitHubPageToolStripMenuItem",
                          lambda: open_url("https://github.com/maximmax42/Discord-CustomRP", self))
        help_menu.addSeparator()
        people = i18n.people()
        self._build_supporters_menu(help_menu.addMenu(form("supportersToolStripMenuItem.Text")), people)
        self._build_translators_menu(help_menu.addMenu(form("translatorsToolStripMenuItem.Text")), people)
        help_menu.addSeparator()
        self._menu_action(help_menu, "checkForUpdatesToolStripMenuItem", lambda: self.check_for_updates(manual=True))
        self._menu_action(help_menu, "aboutToolStripMenuItem", lambda: AboutDialog(self).exec())

        self.download_update_action = self._menu_action(bar, "downloadUpdateToolStripMenuItem",
                                                        lambda: open_url(self.latest_update or RELEASES_PAGE, self))
        self.download_update_action.setVisible(False)

    def _person_action(self, menu, text, url):
        action = menu.addAction(text)
        if url:
            action.setIcon(theme.globe_icon())
            action.setToolTip(url)
            action.triggered.connect(lambda: open_url(url, self))
        return action

    def _build_language_menu(self, menu):
        menu.setToolTipsVisible(True)
        group = QActionGroup(self)
        languages = sorted(i18n.people()["languages"], key=lambda lang: lang["Code"])

        def add(target, text, code, tooltip=None):
            action = target.addAction(text)
            action.setCheckable(True)
            action.setChecked(self.settings.language.lower() == code)
            if tooltip:
                action.setToolTip(tooltip)
            action.triggered.connect(lambda: self.app.change_language(code, self.state != State.DISCONNECTED))
            group.addAction(action)

        add(menu, form("defaultToolStripMenuItem.Text"), "auto")
        menu.addSeparator()

        parts = [menu.addMenu("1/2"), menu.addMenu("2/2")]
        for i, lang in enumerate(languages):
            dialect = f" ({lang['Dialect']})" if lang.get("Dialect") else ""
            part = parts[len(parts) * i // len(languages)]
            part.setToolTipsVisible(True)
            add(part, lang["Name"] + dialect, lang["Code"], lang.get("EnglishName", "") + dialect)

    def _build_translators_menu(self, menu, people):
        languages = sorted(people["languages"], key=lambda lang: lang["Code"])
        progress = {k.lower(): v for k, v in people["progress"].items()}

        parts = [menu.addMenu("1/2"), menu.addMenu("2/2")]
        for i, lang in enumerate(languages):
            if lang["Code"] == "en":
                continue
            dialect = f" ({lang['Dialect']})" if lang.get("Dialect") else ""
            title = f"{lang['Name']} ({lang['EnglishName']}){dialect} - {progress.get(lang['Code'].lower(), '?')}%"
            submenu = parts[len(parts) * i // len(languages)].addMenu(title)
            submenu.setToolTipsVisible(True)
            for person in lang["Translators"]:
                self._person_action(submenu, person["Name"], person.get("Url"))

    def _build_supporters_menu(self, menu, people):
        menu.setToolTipsVisible(True)
        for supporter in sorted(people["supporters"], key=lambda p: float(p["USDAmount"]), reverse=True):
            alt = f" ({supporter['AltAmount']})" if supporter.get("AltAmount") else ""
            self._person_action(menu, f"{supporter['Name']} - ${supporter['USDAmount']}{alt}", supporter.get("Url"))

        menu.addSeparator()

        for supporter in people["nonMonetarySupporters"]:
            title = f"{supporter['Name']} - {supporter['DonationType']}"
            if not supporter.get("DonationUrl"):
                self._person_action(menu, title, supporter.get("Url"))
                continue
            submenu = menu.addMenu(title)
            submenu.setToolTipsVisible(True)
            donation = submenu.addAction(form("openToolStripMenuItem.Text"))
            donation.setToolTip(supporter["DonationUrl"])
            donation.triggered.connect(lambda checked=False, u=supporter["DonationUrl"]: open_url(u, self))
            if supporter.get("Url"):
                self._person_action(submenu, supporter["Name"], supporter["Url"])

    def _build_tray(self):
        self.tray = None
        if not QSystemTrayIcon.isSystemTrayAvailable():
            return

        menu = QMenu(self)
        menu.addAction(form("openToolStripMenuItem.Text"), self.maximize_from_tray)
        menu.addAction(form("trayMenuReconnect.Text"), lambda: self.connect_rpc())
        self.tray_disconnect = menu.addAction(form("buttonDisconnect.Text"), self.disconnect_rpc)
        menu.addAction(form("trayMenuQuit.Text"), self.quit)

        self.tray = QSystemTrayIcon(theme.icon("favicon.ico"), self)
        self.tray.setContextMenu(menu)
        self.tray.activated.connect(self._tray_activated)
        self.tray.messageClicked.connect(self.maximize_from_tray)
        self._set_tray_tooltip()
        self.tray.show()

    def _set_tray_tooltip(self, username=None):
        if not self.tray:
            return
        text = form("trayIcon.Text") + (" (2)" if self.app.second_instance else "")
        self.tray.setToolTip(text + (f"\n{username}" if username else ""))

    # --- Settings <-> fields -----------------------------------------------------

    def _load_fields(self):
        """Puts the settings into the widgets."""
        settings = self.settings
        loading, self.loading = self.loading, True

        for field in self.fields:
            self._edit(field).setText(getattr(settings, field))

        self.type_combo.setCurrentIndex(max(0, self.type_combo.findData(settings.type)))
        self.display_combo.setCurrentIndex(max(0, self.display_combo.findData(settings.display)))
        self.party_size.setValue(settings.partySize)
        self.party_max.setValue(settings.partyMax)

        button = self.timestamp_group.button(settings.timestamps) or \
            self.timestamp_group.button(TimestampType.SINCE_LAST_CONNECTION)
        button.setChecked(True)

        # Default the custom timestamps to now
        for name in ("customTimestamp", "customTimestampEnd"):
            if not getattr(settings, name):
                settings.set_datetime(name, datetime.now())
        for widget, name in ((self.start_dt, "customTimestamp"), (self.end_dt, "customTimestampEnd")):
            widget.setDateTime(QDateTime.fromSecsSinceEpoch(int(settings.get_datetime(name).timestamp())))
        self.end_check.setChecked(settings.customTimestampEndEnabled)

        self.loading = loading
        self._type_changed()
        self._update_timestamp_panel()

    def _field_changed(self, field, text):
        if not self.loading:
            setattr(self.settings, field, text)
            self.save_timer.start()
        if field != "id":
            theme.set_field_color(self.fields[field], "error" if is_field_invalid(field, text) else None)

    def _only_numbers(self, text):
        edit = self.fields["id"]
        digits = "".join(c for c in text if c.isdigit())
        if digits != text:
            cursor = edit.cursorPosition() - (len(text) - len(digits))
            edit.setText(digits)
            edit.setCursorPosition(max(0, cursor))

    def eventFilter(self, obj, event):
        # Trim fields when they lose focus, unless they deliberately start with a no-break space
        if event.type() == QEvent.FocusOut and isinstance(obj, QLineEdit):
            text = obj.text()
            if not text.startswith(NBSP) and text != text.strip():
                obj.setText(text.strip())
        return super().eventFilter(obj, event)

    def _type_changed(self, *_):
        activity_type = self.type_combo.currentData()
        if not self.loading:
            self.settings.type = activity_type
            self.save_settings()
        self.party_panel.setEnabled(activity_type == PLAYING)
        self.timestamp_panel.setEnabled(activity_type != COMPETING)

    def _display_changed(self, *_):
        if not self.loading:
            self.settings.display = self.display_combo.currentData()
            self.save_settings()

    def _party_changed(self, *_):
        if not self.loading:
            self.settings.partySize = self.party_size.value()
            self.settings.partyMax = self.party_max.value()
            self.save_timer.start()

    def _validate_party(self, sender):
        if sender is self.party_max and self.party_max.value() == 0:
            self.party_size.setValue(0)
        elif self.party_size.value() > self.party_max.value():
            self.party_max.setValue(self.party_size.value())
            if sender is self.party_max:
                QApplication.beep()

    def _timestamp_changed(self, kind, checked):
        if not checked or self.loading:
            return
        self.settings.timestamps = kind
        self.save_settings()
        self._update_timestamp_panel()

    def _update_timestamp_panel(self):
        self.custom_panel.setEnabled(self.timestamp_group.checkedId() == TimestampType.CUSTOM)
        self.end_dt.setEnabled(self.end_check.isChecked())

    def _end_toggled(self, checked):
        self.end_dt.setEnabled(checked)
        if not self.loading:
            self.settings.customTimestampEndEnabled = checked
            self.save_settings()

    def _custom_timestamp_changed(self, *_):
        if self.loading:
            return
        self.settings.set_datetime("customTimestamp", self.start_dt.dateTime().toPython())
        self.settings.set_datetime("customTimestampEnd", self.end_dt.dateTime().toPython())
        self.save_timer.start()

    def save_settings(self) -> bool:
        self.save_timer.stop()
        try:
            self.settings.save()
            return True
        except OSError as e:
            error(self, f"{s('errorSavingSettings')} {e}")
            return False

    def _docs_url(self, url):
        return url.replace("docs.customrp.xyz/", "docs.customrp.xyz/" + i18n.docs_prefix())

    # --- Settings menu -----------------------------------------------------------

    def _toggle(self, name, checked):
        setattr(self.settings, name, checked)
        self.save_settings()

    def _toggle_startup(self, checked):
        self._toggle("runOnStartup", checked)
        self._sync_autostart()

    def _sync_autostart(self):
        try:
            if self.settings.runOnStartup != autostart.is_enabled(self.app.second_instance):
                autostart.set_enabled(self.settings.runOnStartup, self.app.second_instance)
        except OSError as e:
            # Let the user simply try again
            self.startup_action.setChecked(not self.settings.runOnStartup)
            self.settings.runOnStartup = not self.settings.runOnStartup
            error(self, f"{s('errorStartupShortcut')} {e}")

    def _toggle_check_updates(self, checked):
        self._toggle("checkUpdates", checked)
        if checked:
            self.check_for_updates(manual=False)

    def _toggle_dark_mode(self, checked):
        self._toggle("darkMode", checked)
        theme.apply(QApplication.instance(), checked)
        for action in self.findChildren(QAction):
            if action.toolTip().startswith("http") and not action.icon().isNull():
                action.setIcon(theme.globe_icon())
        self._set_state(self.state, self.status_label.text())
        for field in self.fields:
            if field != "id":
                self._field_changed(field, self._edit(field).text())

    # --- Connection --------------------------------------------------------------

    def _set_state(self, state, status=None):
        self.state = state
        default_status = {
            State.DISCONNECTED: "statusDisconnected",
            State.CONNECTING: "statusConnecting",
            State.UPDATING: "statusUpdatingPresence",
            State.CONNECTED: "statusConnected",
            State.ERROR: "statusError",
        }
        self.status_label.setText(status or s(default_status[state]))
        color = {State.CONNECTED: "success", State.ERROR: "error"}.get(state)
        theme.set_field_color(self.fields["id"], color)

        connected = state != State.DISCONNECTED
        self.connect_button.setEnabled(not connected)
        self.disconnect_button.setEnabled(connected)
        if self.tray:
            self.tray_disconnect.setEnabled(connected)
        if not connected:
            self.update_button.setEnabled(False)

    def _app_id(self):
        return self.settings.id or DEFAULT_ID

    def _start_client(self):
        if self.client:
            self.client.close()

        client = DiscordIPC(self._app_id(), self.settings.pipe)
        client.on_ready = lambda user: self.bridge.ready.emit(client, user)
        client.on_presence_update = lambda activity: self.bridge.presence_updated.emit(client, activity)
        client.on_error = lambda code, message: self.bridge.error.emit(client, code, message)
        client.on_connection_failed = lambda message: self.bridge.connection_failed.emit(client, message)
        self.client = client
        client.start()

    def _connect_clicked(self):
        mods = QApplication.keyboardModifiers()
        if mods & Qt.ControlModifier and mods & Qt.ShiftModifier:
            PipeDialog(self.settings, self).exec()
            return
        self.connect_rpc()

    def connect_rpc(self, retrying=False):
        self.save_settings()
        self.retrying = retrying
        self._start_client()
        self.connected_at = datetime.now(timezone.utc)
        self._set_state(State.CONNECTING)

    def disconnect_rpc(self):
        self.restart_timer.stop()
        self.local_time_timer.stop()
        self.restart_attempts_left = -1
        if self.client:
            self.client.close()
            self.client = None
        self.username_label.setText("")
        self._set_tray_tooltip()
        self._set_state(State.DISCONNECTED)

    def reconnect(self):
        self.restart_timer.stop()
        self.local_time_timer.stop()
        self.save_settings()
        self._start_client()
        self._set_state(State.CONNECTING)

    def _restart_elapsed(self):
        if self.restart_attempts_left == 0:
            self.disconnect_rpc()
            return
        if self.restart_attempts_left == -1:
            self.restart_attempts_left = RESTART_ATTEMPTS
        self.restart_attempts_left -= 1
        self.connect_rpc(retrying=True)

    def _update_clicked(self):
        self.save_settings()
        if self.client and self.client.client_id == self._app_id():
            self.set_presence()
        else:
            self.reconnect()

    def _on_ready(self, client, user):
        if client is not self.client:
            return
        name = display_name(user)
        self.username_label.setText(name)
        self._set_tray_tooltip(name)
        self.update_button.setEnabled(True)
        self.set_presence()

    def _on_presence_updated(self, client, activity):
        if client is not self.client:
            return
        self.restart_timer.stop()
        self.restart_attempts_left = -1
        self.retrying = False
        self._set_state(State.CONNECTED)

    def _on_error(self, client, code, message):
        if client is not self.client or self.state == State.DISCONNECTED:
            return
        was_retrying = self.retrying
        self._set_state(State.ERROR)
        self.restart_timer.start()
        # Don't show the same error every 10 seconds while reconnecting
        if not was_retrying:
            error(self, message)

    def _on_connection_failed(self, client, message):
        if client is not self.client or self.state == State.DISCONNECTED:
            return
        self._set_state(State.ERROR, s("statusConnectionFailed"))
        self.restart_timer.start()

    def set_presence(self) -> bool:
        if not self.client or self.client.closed:
            return False

        self.local_time_timer.stop()

        try:
            normalize_settings(self.settings)
            activity, refresh_in = build_activity(self.settings, connected_at=self.connected_at,
                                                  started_at=self.started_at)
        except PresenceError as e:
            message = s(e.key)
            if e.field:
                message += f" ({form(e.field + '.Text')})"
            if e.detail:
                message += f"\n{e.detail}"
            error(self, message)
            return False
        finally:
            self._load_fields()
            self.save_settings()

        self.client.set_activity(activity)
        self._set_state(State.UPDATING)

        if refresh_in is not None:
            self.local_time_timer.start(int(refresh_in * 1000))
        return True

    # --- Assets ------------------------------------------------------------------

    def _fetch_assets(self, combo):
        app_id = self.settings.id
        if not app_id or (app_id == self.last_assets_id and self.next_assets_check > datetime.now()):
            return

        self.last_assets_id = app_id
        self.next_assets_check = datetime.now().replace(year=9999)  # No new requests while this one runs

        def done(data, err):
            if err:
                self.next_assets_check = datetime.min
                log.warning("Couldn't fetch assets: %s", err)
                error(self, s("errorNoInternet"))
                return
            self.next_assets_check = datetime.now() + timedelta(minutes=1)
            self._set_assets([asset.get("name", "") for asset in json.loads(data)])
            if combo.hasFocus() or combo.lineEdit().hasFocus():
                combo.showPopup()

        run_in_background(lambda: http_get(f"https://discord.com/api/oauth2/applications/{app_id}/assets"), done)

    def _set_assets(self, names):
        for field in ("largeKey", "smallKey"):
            combo = self.fields[field]
            text = combo.currentText()
            combo.blockSignals(True)
            combo.clear()
            combo.addItems(names)
            combo.setEditText(text)
            combo.blockSignals(False)

    def open_assets_page(self):
        if not self.settings.id:
            error(self, s("errorNoID"))
            return
        open_url(f"https://discord.com/developers/applications/{self.settings.id}/rich-presence/assets", self)

    # --- Presets -----------------------------------------------------------------

    def new_preset(self):
        if not ask(self, QApplication.applicationName(), s("newPresetConfirmation")):
            return
        if self.state != State.DISCONNECTED:
            self.disconnect_rpc()

        for field in self.fields:
            setattr(self.settings, field, "")
        self.settings.type = PLAYING
        self.settings.display = 0
        self.settings.partySize = self.settings.partyMax = 0
        self.settings.timestamps = TimestampType.SINCE_LAST_CONNECTION
        self.settings.customTimestampEndEnabled = False
        self.save_settings()
        self._load_fields()

    def load_preset_file(self, path):
        was_connected = self.state != State.DISCONNECTED
        old_id = self.settings.id

        try:
            load_preset(path, self.settings)
        except PresetError as e:
            log.warning("Couldn't load preset %s: %s", path, e)
            error(self, s("errorInvalidPresetFile"))
            return

        self.save_settings()
        self._load_fields()

        if not was_connected:
            return
        if self.settings.id != old_id or self.state != State.CONNECTED:
            self.reconnect()
        else:
            self.set_presence()

    def load_preset_dialog(self):
        path, _ = QFileDialog.getOpenFileName(self, form("loadPresetToolStripMenuItem.Text"), "", PRESET_FILTER)
        if path:
            self.load_preset_file(path)

    def save_preset_dialog(self):
        path, _ = QFileDialog.getSaveFileName(self, form("savePresetToolStripMenuItem.Text"), "", PRESET_FILTER)
        if not path:
            return
        if not path.lower().endswith(".crp"):
            path += ".crp"

        while True:
            try:
                save_preset(path, self.settings)
                return
            except OSError as e:
                answer = QMessageBox.critical(self, s("error"), str(e), QMessageBox.Retry | QMessageBox.Cancel)
                if answer == QMessageBox.Cancel:
                    return

    def dragEnterEvent(self, event):
        if event.mimeData().hasUrls():
            event.acceptProposedAction()

    def dropEvent(self, event):
        urls = [u for u in event.mimeData().urls() if u.isLocalFile()]
        if urls:
            # If multiple files are dropped, only the first one gets imported
            self.load_preset_file(urls[0].toLocalFile())

    # --- Updates -----------------------------------------------------------------

    def check_for_updates(self, manual):
        def done(update, err):
            if err:
                log.warning("Update check failed: %s", err)
                if manual:
                    error(self, str(err))
                return
            if not update:
                if manual:
                    info(self, s("noUpdatesFound"))
                return
            if update.tag == self.settings.ignoreVersion and not manual:
                return

            self.latest_update = update.url
            self.download_update_action.setVisible(True)
            self.maximize_from_tray()
            QApplication.beep()

            result = UpdateDialog(update, self.settings, self).exec()
            if result == UpdateDialog.Accepted:
                open_url(update.url, self)
            elif result == UpdateDialog.SKIP:
                self.settings.ignoreVersion = update.tag
                self.save_settings()

            self.check_updates_action.setChecked(self.settings.checkUpdates)
            if not self.settings.checkUpdates or result == UpdateDialog.SKIP:
                self.download_update_action.setVisible(False)

        run_in_background(check_updates, done)

    # --- Window and tray ---------------------------------------------------------

    def _tray_activated(self, reason):
        if reason in (QSystemTrayIcon.Trigger, QSystemTrayIcon.DoubleClick):
            self.maximize_from_tray()

    def maximize_from_tray(self):
        self.show()
        self.setWindowState(self.windowState() & ~Qt.WindowMinimized)
        self.raise_()
        self.activateWindow()

    def closeEvent(self, event):
        if self.quitting or not self.tray:
            self.save_settings()
            event.accept()
            if not self.quitting:
                self.quit()
            return

        event.ignore()
        self.hide()
        if not (self.settings.startMinimized or self.tooltip_shown):
            self.tray.showMessage(form("trayIcon.BalloonTipTitle"), form("trayIcon.BalloonTipText"),
                                  theme.icon("favicon.ico"), 5000)
            self.tooltip_shown = True

    def shutdown(self):
        """Stops timers and the connection, and removes the tray icon."""
        self.quitting = True
        self.restart_timer.stop()
        self.local_time_timer.stop()
        if self.client:
            self.client.close()
            self.client = None
        if self.tray:
            self.tray.hide()
        self.save_settings()

    def quit(self):
        if self.save_settings():
            self.shutdown()
            QApplication.quit()
