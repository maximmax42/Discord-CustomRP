"""Per-platform locations for settings, logs and bundled resources."""

import os
import sys
from pathlib import Path

PACKAGE_DIR = Path(__file__).resolve().parent
ASSETS_DIR = PACKAGE_DIR / "assets"
LOCALES_DIR = PACKAGE_DIR / "locales"
DATA_DIR = PACKAGE_DIR / "data"


def user_dir() -> Path:
    """Directory that holds settings and logs for the current user."""
    if sys.platform == "win32":
        # Not %APPDATA%\CustomRP: that's where the original Windows installer puts the app itself
        base = Path(os.environ.get("LOCALAPPDATA") or Path.home() / "AppData" / "Local")
        return base / "CustomRP-Python"
    if sys.platform == "darwin":
        return Path.home() / "Library" / "Application Support" / "CustomRP"

    base = os.environ.get("XDG_CONFIG_HOME") or Path.home() / ".config"
    return Path(base) / "customrp"


def settings_file(second_instance: bool) -> Path:
    return user_dir() / ("settings-2.json" if second_instance else "settings.json")


def logs_dir() -> Path:
    return user_dir() / "logs"


def crash_log() -> Path:
    return user_dir() / "crash.log"
