"""Turns the settings into a Discord activity payload."""

import re
from datetime import datetime, timedelta
from urllib.parse import quote, urlsplit, urlunsplit

from .settings import Settings

DEFAULT_ID = "896771305108553788"

# Activity types, in the order they're shown in the UI: (Strings key, Discord value)
ACTIVITY_TYPES = [
    ("presenceTypePlaying", 0),
    ("presenceTypeListening", 2),
    ("presenceTypeWatching", 3),
    ("presenceTypeCompeting", 5),
]
PLAYING, COMPETING = 0, 5

# Which field shows up in the member list: (name, Discord value)
DISPLAY_TYPES = [("Name", 0), ("Details", 2), ("State", 1)]


class TimestampType:
    # Values are stored in settings and presets, so they must not change
    SINCE_LAST_CONNECTION = 0
    SINCE_STARTUP = 1
    LOCAL_TIME = 2
    CUSTOM = 3
    SINCE_PRESENCE_UPDATE = 4


# Maximum lengths of the fields, same as in the Windows app
MAX_LENGTH = {
    "id": 19,
    "name": 256,
    "details": 128,
    "detailsURL": 256,
    "state": 128,
    "stateURL": 256,
    "largeKey": 256,
    "largeText": 128,
    "largeURL": 256,
    "smallKey": 256,
    "smallText": 128,
    "smallURL": 256,
    "button1Text": 31,
    "button1URL": 512,
    "button2Text": 31,
    "button2URL": 512,
}

# Fields Discord measures in UTF-8 bytes rather than characters
BYTE_LIMITED = {"details", "state", "largeText", "smallText", "button1Text", "button2Text"}

NBSP = " "
ZWSP = "​"


class PresenceError(ValueError):
    """The presence can't be sent as is.

    ``key`` names an entry of the Strings table; ``field`` optionally names the UI label of the
    field at fault (for example "labelLarge").
    """

    def __init__(self, key: str, field: str = None, detail: str = None):
        super().__init__(detail or key)
        self.key = key
        self.field = field
        self.detail = detail


def is_field_invalid(name: str, text: str) -> bool:
    """Whether a text field's content would be rejected by Discord."""
    if len(text) == 1:  # Discord requires at least 2 characters
        return True
    return name in BYTE_LIMITED and len(text.encode("utf-8")) > MAX_LENGTH[name]


def protect_leading_space(text: str, max_length: int) -> str:
    """Discord trims leading whitespace, but not if a zero-width space comes first."""
    if text.startswith(NBSP) and len(text) < max_length:
        return ZWSP + text
    if text.startswith(NBSP + NBSP):
        return ZWSP + text[1:]
    return text


def _encode_url(url: str) -> str:
    """Converts the host to punycode and percent-encodes characters that aren't allowed in URLs."""
    try:
        parts = urlsplit(url)
        host = parts.hostname
    except ValueError:
        return url

    if not host:
        return url

    try:
        ascii_host = host.encode("idna").decode("ascii")
    except UnicodeError:
        ascii_host = host

    netloc = ascii_host
    if parts.port:
        netloc += f":{parts.port}"
    if parts.username:
        userinfo = parts.username + (f":{parts.password}" if parts.password else "")
        netloc = f"{userinfo}@{netloc}"

    path = quote(parts.path, safe="/%:@!$&'()*+,;=-._~")
    query = quote(parts.query, safe="/%:@!$&'()*+,;=-._~?")
    fragment = quote(parts.fragment, safe="/%:@!$&'()*+,;=-._~?")

    return urlunsplit((parts.scheme.lower(), netloc, path, query, fragment))


def process_url(url: str, max_length: int) -> str:
    if not url:
        return url
    if "://" not in url:
        url = "https://" + url
    return _encode_url(url)[:max_length]


def _mp_external_over_limit(url: str) -> bool:
    """Discord stores image URLs as "mp:external/<id>/<query>/<scheme>/<host><path>", capped at 256 characters."""
    parts = urlsplit(url)
    host = parts.hostname or ""
    if host == "media.discordapp.net":
        host = "cdn.discordapp.com"
    query = "?" + parts.query if parts.query else ""
    return len(f"mp:external/{'x' * 43}/{quote(query, safe='')}/{parts.scheme}/{host}{parts.path}") > 256


def process_image_key(key: str, field: str) -> str:
    """Normalizes an image key; keys that are URLs get encoded and length-checked."""
    if "://" not in key:
        return key
    url = _encode_url(key)
    if _mp_external_over_limit(url):
        raise PresenceError("errorInvalidImageURL", field)
    return url


def proxify(key: str) -> str:
    """Discord refuses to show images from its own CDN, so those go through customrp.xyz."""
    return re.sub(r"//(cdn|media)\.discordapp\.(com|net)/", "//customrp.xyz/proxy/", key)


def normalize_settings(settings: Settings):
    """Cleans up the user's input in place, the same way the Windows app does before sending it."""
    for name in ("details", "state"):
        setattr(settings, name, protect_leading_space(getattr(settings, name), MAX_LENGTH[name]))

    if settings.partySize > settings.partyMax:
        settings.partyMax = settings.partySize

    for name in ("detailsURL", "stateURL", "largeKey", "largeURL", "smallKey", "smallURL", "button1URL", "button2URL"):
        setattr(settings, name, getattr(settings, name).strip())

    for name in ("detailsURL", "stateURL", "largeURL", "smallURL", "button1URL", "button2URL"):
        setattr(settings, name, process_url(getattr(settings, name), MAX_LENGTH[name]))

    settings.largeKey = process_image_key(settings.largeKey, "labelLarge")
    settings.smallKey = process_image_key(settings.smallKey, "labelSmall")


def _ms(moment: datetime) -> int:
    return int(moment.timestamp() * 1000)


def build_activity(settings: Settings, *, connected_at: datetime, started_at: datetime, now: datetime = None):
    """Builds the SET_ACTIVITY payload.

    Returns ``(activity, refresh_in)``, where ``refresh_in`` is the number of seconds after which
    the presence must be sent again (for the local time timestamp), or ``None``.
    Call :func:`normalize_settings` first.
    """
    now = now or datetime.now().astimezone()

    for name in BYTE_LIMITED:
        value = getattr(settings, name)
        if len(value.encode("utf-8")) > MAX_LENGTH[name]:
            raise PresenceError("errorInvalidURL" if name.startswith("button") else "error", None,
                                f"{name}: {len(value.encode('utf-8'))}/{MAX_LENGTH[name]} bytes")

    activity = {"type": settings.type, "status_display_type": settings.display, "instance": False}

    def put(key, value):
        if value:
            activity[key] = value

    put("name", settings.name)
    put("details", settings.details)
    put("details_url", settings.detailsURL)
    put("state", settings.state)
    put("state_url", settings.stateURL)

    assets = {}
    for prefix in ("large", "small"):
        key = getattr(settings, prefix + "Key")
        if key:
            assets[prefix + "_image"] = proxify(key)
        for suffix, attr in (("_text", "Text"), ("_url", "URL")):
            value = getattr(settings, prefix + attr)
            if value:
                assets[prefix + suffix] = value
    if assets:
        activity["assets"] = assets

    if settings.type == PLAYING and settings.partySize > 0 and settings.partyMax > 0:
        activity["party"] = {"id": "CustomRP", "size": [settings.partySize, settings.partyMax]}

    buttons = []
    for n in (1, 2):
        text = getattr(settings, f"button{n}Text")
        url = getattr(settings, f"button{n}URL")
        if text and url:
            buttons.append({"label": text, "url": url})
    if buttons:
        activity["buttons"] = buttons

    refresh_in = None
    timestamps = {}

    if settings.type != COMPETING:
        kind = settings.timestamps
        if kind == TimestampType.SINCE_LAST_CONNECTION:
            timestamps["start"] = _ms(connected_at)
        elif kind == TimestampType.SINCE_STARTUP:
            timestamps["start"] = _ms(started_at)
        elif kind == TimestampType.SINCE_PRESENCE_UPDATE:
            timestamps["start"] = _ms(now)
        elif kind == TimestampType.LOCAL_TIME:
            midnight = now.replace(hour=0, minute=0, second=0, microsecond=0)
            timestamps["start"] = _ms(midnight)
            refresh_in = ((midnight + timedelta(days=1, seconds=5)) - now).total_seconds()
        elif kind == TimestampType.CUSTOM:
            start = settings.get_datetime("customTimestamp").astimezone()
            if settings.customTimestampEndEnabled:
                timestamps["start"] = _ms(start)
                timestamps["end"] = _ms(settings.get_datetime("customTimestampEnd").astimezone())
            elif start < now:
                timestamps["start"] = _ms(start)
            else:
                timestamps["start"] = _ms(now)
                timestamps["end"] = _ms(start)

    if timestamps:
        activity["timestamps"] = timestamps

    return activity, refresh_in
