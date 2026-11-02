"""Run on startup, for each platform.

* Linux: an XDG autostart .desktop file
* macOS: a LaunchAgent plist
* Windows: a value under HKCU\\...\\CurrentVersion\\Run
"""

import os
import plistlib
import shlex
import subprocess
import sys
from pathlib import Path


def launch_command(second_instance: bool) -> list:
    """The command that starts this app the way it's currently installed."""
    if getattr(sys, "frozen", False):  # PyInstaller and similar
        command = [sys.executable]
    else:
        python = Path(sys.executable)
        if sys.platform == "win32" and python.name.lower() == "python.exe":
            # pythonw doesn't open a console window
            pythonw = python.with_name("pythonw.exe")
            if pythonw.exists():
                python = pythonw
        command = [str(python), "-m", "customrp"]

    if second_instance:
        command.append("--second-instance")
    return command


def _linux_file(second_instance):
    base = Path(os.environ.get("XDG_CONFIG_HOME") or Path.home() / ".config")
    return base / "autostart" / ("customrp-2.desktop" if second_instance else "customrp.desktop")


def _mac_label(second_instance):
    return "xyz.customrp.CustomRP" + (".2" if second_instance else "")


def _mac_file(second_instance):
    return Path.home() / "Library" / "LaunchAgents" / f"{_mac_label(second_instance)}.plist"


def _win_value(second_instance):
    return "CustomRP (Python)" + (" 2" if second_instance else "")


_RUN_KEY = r"Software\Microsoft\Windows\CurrentVersion\Run"


def is_enabled(second_instance: bool) -> bool:
    if sys.platform == "win32":
        import winreg
        try:
            with winreg.OpenKey(winreg.HKEY_CURRENT_USER, _RUN_KEY) as key:
                winreg.QueryValueEx(key, _win_value(second_instance))
            return True
        except OSError:
            return False
    if sys.platform == "darwin":
        return _mac_file(second_instance).exists()
    return _linux_file(second_instance).exists()


def set_enabled(enabled: bool, second_instance: bool):
    """Adds or removes the startup entry. Raises OSError on failure."""
    command = launch_command(second_instance)

    if sys.platform == "win32":
        import winreg
        with winreg.CreateKey(winreg.HKEY_CURRENT_USER, _RUN_KEY) as key:
            if enabled:
                winreg.SetValueEx(key, _win_value(second_instance), 0, winreg.REG_SZ, subprocess.list2cmdline(command))
            else:
                try:
                    winreg.DeleteValue(key, _win_value(second_instance))
                except FileNotFoundError:
                    pass
        return

    if sys.platform == "darwin":
        path = _mac_file(second_instance)
        if enabled:
            path.parent.mkdir(parents=True, exist_ok=True)
            with open(path, "wb") as f:
                plistlib.dump({
                    "Label": _mac_label(second_instance),
                    "ProgramArguments": command,
                    "RunAtLoad": True,
                    "ProcessType": "Interactive",
                }, f)
        elif path.exists():
            path.unlink()
        return

    path = _linux_file(second_instance)
    if enabled:
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(
            "[Desktop Entry]\n"
            "Type=Application\n"
            "Name=CustomRP" + (" 2" if second_instance else "") + "\n"
            "Comment=Discord Custom Rich Presence Manager\n"
            f"Exec={shlex.join(command)}\n"
            "Icon=customrp\n"
            "Terminal=false\n"
            "X-GNOME-Autostart-enabled=true\n",
            encoding="utf-8",
        )
    elif path.exists():
        path.unlink()
