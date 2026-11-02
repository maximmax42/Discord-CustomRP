"""Dark and light color schemes, matching the Windows app's Themes.cs."""

from PySide6.QtGui import QColor, QIcon, QPalette
from PySide6.QtWidgets import QApplication, QWidget

from ..paths import ASSETS_DIR

DARK = {
    "bg": "#37393f",
    "field": "#41444b",
    "success": "#329632",
    "error": "#963232",
    "text": "#ffffff",
    "menu": "#2f3136",
    "hover": "#34373c",
    "active": "#393c43",
    "check": "#3d49a2",
    "inactive": "#8e9297",
}

LIGHT = {
    "bg": "#ffffff",
    "field": "#ebedef",
    "success": "#c0ffc0",
    "error": "#ffc0c0",
    "text": "#000000",
    "menu": "#f2f3f5",
    "hover": "#e3e5e8",
    "active": "#d4d7dc",
    "check": "#5865f2",
    "inactive": "#8e9297",
}

colors = DARK


def apply(app: QApplication, dark: bool):
    global colors
    colors = DARK if dark else LIGHT

    app.setStyle("Fusion")

    p = QPalette()
    for group in (QPalette.Active, QPalette.Inactive):
        p.setColor(group, QPalette.Window, QColor(colors["bg"]))
        p.setColor(group, QPalette.WindowText, QColor(colors["text"]))
        p.setColor(group, QPalette.Base, QColor(colors["field"]))
        p.setColor(group, QPalette.AlternateBase, QColor(colors["menu"]))
        p.setColor(group, QPalette.Text, QColor(colors["text"]))
        p.setColor(group, QPalette.Button, QColor(colors["field"]))
        p.setColor(group, QPalette.ButtonText, QColor(colors["text"]))
        p.setColor(group, QPalette.ToolTipBase, QColor(colors["menu"]))
        p.setColor(group, QPalette.ToolTipText, QColor(colors["text"]))
        p.setColor(group, QPalette.Highlight, QColor(colors["check"]))
        p.setColor(group, QPalette.HighlightedText, QColor("#ffffff"))
        p.setColor(group, QPalette.PlaceholderText, QColor(colors["inactive"]))
        p.setColor(group, QPalette.Link, QColor("#00a8fc" if dark else "#006ce7"))

    for role in (QPalette.WindowText, QPalette.Text, QPalette.ButtonText):
        p.setColor(QPalette.Disabled, role, QColor(colors["inactive"]))
    p.setColor(QPalette.Disabled, QPalette.Base, QColor(colors["bg"]))
    p.setColor(QPalette.Disabled, QPalette.Button, QColor(colors["bg"]))
    p.setColor(QPalette.Disabled, QPalette.Window, QColor(colors["bg"]))

    app.setPalette(p)
    app.setStyleSheet(f"""
        QMenuBar {{ background: {colors['menu']}; }}
        QMenuBar::item:selected {{ background: {colors['hover']}; }}
        QMenu {{ background: {colors['menu']}; border: 1px solid {colors['active']}; }}
        QMenu::item:selected {{ background: {colors['active']}; }}
        QMenu::separator {{ height: 1px; background: {colors['active']}; margin: 4px 6px; }}
        QStatusBar {{ background: {colors['menu']}; }}
        QFrame#separator {{ background: {colors['text']}; max-height: 1px; min-height: 1px; border: none; }}
    """)


def set_field_color(widget: QWidget, state: str = None):
    """Tints an input's background: "success", "error", or None for the normal color."""
    p = widget.palette()
    color = QColor(colors[state or "field"])
    for group in (QPalette.Active, QPalette.Inactive):
        p.setColor(group, QPalette.Base, color)
        p.setColor(group, QPalette.Button, color)
    widget.setPalette(p)


def icon(name: str) -> QIcon:
    return QIcon(str(ASSETS_DIR / name))


def globe_icon() -> QIcon:
    return icon("globe_white.png" if colors is DARK else "globe.png")
