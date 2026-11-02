"""UI translations, generated from the Windows app's .resx files by tools/convert_resx.py."""

import json

from .paths import DATA_DIR, LOCALES_DIR

_strings = {}
_fallback = {}
_current = "en"

# Locales docs.customrp.xyz is translated to
DOCS_LOCALES = ["de", "es", "fi", "fr", "hi", "ko", "nl", "pl", "ro", "ru", "th", "uk", "vi"]


def _read(code):
    try:
        with open(LOCALES_DIR / f"{code}.json", encoding="utf-8") as f:
            return json.load(f)
    except (OSError, ValueError):
        return {}


def available():
    return sorted(p.stem for p in LOCALES_DIR.glob("*.json"))


def resolve(setting: str, system_languages) -> str:
    """Picks a locale for the language setting; "auto" walks the system's preferred languages."""
    codes = set(available())

    if setting != "auto" and setting.lower() in codes:
        return setting.lower()

    for lang in system_languages:
        lang = lang.replace("_", "-").lower()
        if lang.startswith("zh"):
            # zh-TW, zh-HK and zh-Hant-* use traditional characters
            traditional = any(part in ("hant", "tw", "hk", "mo") for part in lang.split("-")[1:])
            return "zh-hant" if traditional else "zh-hans"
        if lang in codes:
            return lang
        base = lang.split("-")[0]
        if base == "nb" or base == "nn":
            base = "no"
        if base in codes:
            return base

    return "en"


def load(code: str):
    global _strings, _fallback, _current
    _fallback = _read("en")
    _strings = _read(code) if code != "en" else {}
    # Regional variants fall back to the base language first, like .NET resources do
    if "-" in code:
        _strings = {**_read(code.split("-")[0]), **_strings}
    _current = code


def current() -> str:
    return _current


def is_rtl() -> bool:
    return bool(_strings.get("_rtl"))


def tr(key: str, default: str = None) -> str:
    return _strings.get(key) or _fallback.get(key) or (default if default is not None else key)


def s(key: str) -> str:
    """A string from the Strings table (messages)."""
    return tr("Strings." + key)


def form(key: str) -> str:
    """A string from the main window's resources, like "labelID.Text"."""
    return tr("MainForm." + key)


def docs_prefix() -> str:
    """Path prefix for docs.customrp.xyz links in the current language."""
    for loc in reversed(DOCS_LOCALES):
        if loc == _current or loc == _current.split("-")[0]:
            return f"v/{loc}/"
    return ""


def people():
    with open(DATA_DIR / "people.json", encoding="utf-8") as f:
        return json.load(f)
