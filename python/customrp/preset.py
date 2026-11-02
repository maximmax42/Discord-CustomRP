"""Reading and writing .crp preset files.

The format is what .NET's XmlSerializer produces for the Windows app's Preset struct, so presets
can be shared between both versions.
"""

import re
import xml.etree.ElementTree as ET
from datetime import datetime

from .settings import Settings

# (XML element, settings field, type)
FIELDS = [
    ("ID", "id", str),
    ("Type", "type", int),
    ("Display", "display", int),
    ("Name", "name", str),
    ("Details", "details", str),
    ("DetailsURL", "detailsURL", str),
    ("State", "state", str),
    ("StateURL", "stateURL", str),
    ("PartySize", "partySize", int),
    ("PartyMax", "partyMax", int),
    ("Timestamps", "timestamps", int),
    ("CustomTimestamp", "customTimestamp", datetime),
    ("CustomTimestampEndEnabled", "customTimestampEndEnabled", bool),
    ("CustomTimestampEnd", "customTimestampEnd", datetime),
    ("LargeKey", "largeKey", str),
    ("LargeText", "largeText", str),
    ("LargeURL", "largeURL", str),
    ("SmallKey", "smallKey", str),
    ("SmallText", "smallText", str),
    ("SmallURL", "smallURL", str),
    ("Button1Text", "button1Text", str),
    ("Button1URL", "button1URL", str),
    ("Button2Text", "button2Text", str),
    ("Button2URL", "button2URL", str),
]


class PresetError(Exception):
    pass


def parse_xml_datetime(text: str) -> str:
    """Converts an xs:dateTime to the naive local ISO string used in settings ("" if unset)."""
    m = re.fullmatch(r"(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2})(?:\.(\d+))?(Z|[+-]\d{2}:\d{2})?", text.strip())
    if not m:
        raise PresetError(f"Bad date: {text!r}")

    base, _fraction, zone = m.groups()
    if base.startswith("0001-01-01"):  # DateTime.MinValue, i.e. never set
        return ""

    moment = datetime.fromisoformat(base)
    if zone:
        moment = datetime.fromisoformat(base + ("+00:00" if zone == "Z" else zone)).astimezone().replace(tzinfo=None)
    return moment.isoformat()


def load_preset(path, settings: Settings):
    """Applies a preset file to the settings. Raises PresetError if the file can't be used."""
    try:
        root = ET.parse(path).getroot()
    except (OSError, ET.ParseError) as e:
        raise PresetError(str(e)) from e

    if root.tag != "Preset":
        raise PresetError(f"Unexpected root element {root.tag!r}")

    values = {}
    for element, field, kind in FIELDS:
        node = root.find(element)
        text = node.text or "" if node is not None else None

        try:
            if kind is str:
                values[field] = text or ""
            elif kind is int:
                values[field] = int(text) if text else 0
            elif kind is bool:
                values[field] = text == "true"
            elif kind is datetime:
                values[field] = parse_xml_datetime(text) if text else ""
        except ValueError as e:
            raise PresetError(f"{element}: {e}") from e

    for field, value in values.items():
        setattr(settings, field, value)


def save_preset(path, settings: Settings):
    root = ET.Element("Preset", {
        "xmlns:xsi": "http://www.w3.org/2001/XMLSchema-instance",
        "xmlns:xsd": "http://www.w3.org/2001/XMLSchema",
    })

    for element, field, kind in FIELDS:
        value = getattr(settings, field)
        node = ET.SubElement(root, element)

        if kind is bool:
            node.text = "true" if value else "false"
        elif kind is datetime:
            node.text = settings.get_datetime(field).astimezone().isoformat()
        else:
            node.text = str(value)

    ET.indent(root, space="  ")
    with open(path, "wb") as f:
        f.write(b'<?xml version="1.0"?>\n')
        f.write(ET.tostring(root, encoding="utf-8"))
