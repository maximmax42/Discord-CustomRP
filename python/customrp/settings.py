"""Application settings, stored as JSON.

Field names match the original C# Settings.settings so the meaning of each value carries over.
"""

import json
import os
from dataclasses import asdict, dataclass, fields
from datetime import datetime
from pathlib import Path


class CorruptSettingsError(Exception):
    """Raised when the settings file exists but can't be parsed."""

    def __init__(self, path: Path, cause: Exception):
        super().__init__(f"{path}: {cause}")
        self.path = path


@dataclass
class Settings:
    id: str = ""
    type: int = 0
    display: int = 0
    name: str = ""
    details: str = ""
    detailsURL: str = ""
    state: str = ""
    stateURL: str = ""
    partySize: int = 0
    partyMax: int = 0
    timestamps: int = 0
    # Stored as naive local time in ISO format; empty means "not set yet"
    customTimestamp: str = ""
    customTimestampEndEnabled: bool = False
    customTimestampEnd: str = ""
    largeKey: str = ""
    largeText: str = ""
    largeURL: str = ""
    smallKey: str = ""
    smallText: str = ""
    smallURL: str = ""
    button1Text: str = ""
    button1URL: str = ""
    button2Text: str = ""
    button2URL: str = ""
    startMinimized: bool = False
    # The Windows app defaults to True; off here so running from source doesn't silently register itself
    runOnStartup: bool = False
    firstStart: bool = True
    checkUpdates: bool = True
    ignoreVersion: str = ""
    language: str = "auto"
    pipe: int = -1
    autoconnect: bool = True
    darkMode: bool = True

    path: Path = None  # Not saved

    # --- Persistence -----------------------------------------------------------

    @classmethod
    def load(cls, path: Path) -> "Settings":
        settings = cls()
        settings.path = path

        if not path.exists():
            return settings

        try:
            with open(path, encoding="utf-8") as f:
                data = json.load(f)
            if not isinstance(data, dict):
                raise ValueError("Settings file doesn't contain an object")
        except (OSError, ValueError) as e:
            raise CorruptSettingsError(path, e) from e

        for field in fields(cls):
            if field.name == "path" or field.name not in data:
                continue
            value = data[field.name]
            # Ignore values of the wrong type instead of failing on them (bool and int are told apart too)
            if type(value) is type(getattr(settings, field.name)):
                setattr(settings, field.name, value)

        return settings

    def save(self):
        data = asdict(self)
        del data["path"]

        self.path.parent.mkdir(parents=True, exist_ok=True)
        tmp = self.path.with_suffix(".tmp")
        with open(tmp, "w", encoding="utf-8") as f:
            json.dump(data, f, ensure_ascii=False, indent=2)
        os.replace(tmp, self.path)

    # --- Helpers ---------------------------------------------------------------

    def get_datetime(self, name: str) -> datetime:
        value = getattr(self, name)
        try:
            return datetime.fromisoformat(value) if value else datetime.now().replace(microsecond=0)
        except ValueError:
            return datetime.now().replace(microsecond=0)

    def set_datetime(self, name: str, value: datetime):
        setattr(self, name, value.replace(tzinfo=None, microsecond=0).isoformat())
