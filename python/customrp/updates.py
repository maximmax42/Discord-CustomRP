"""Checking GitHub for newer releases."""

import json
import urllib.request
from dataclasses import dataclass

from . import __version__

RELEASES_API = "https://api.github.com/repos/maximmax42/Discord-CustomRP/releases"
RELEASES_PAGE = "https://github.com/maximmax42/Discord-CustomRP/releases/latest"


def parse_version(text: str) -> tuple:
    """"v1.2" -> (1, 2, 0, 0). Raises ValueError for anything that isn't 2 to 4 numbers."""
    parts = text.strip().lstrip("v").split(".")
    if not 2 <= len(parts) <= 4:
        raise ValueError(f"Version has {len(parts)} part(s)!")
    numbers = [int(p) for p in parts]
    return tuple(numbers + [0] * (4 - len(numbers)))


def version_string(version: tuple) -> str:
    """(2, 3, 0, 0) -> "2.3"; keeps the build number only when it or the revision is set."""
    major, minor, build, revision = version
    result = f"{major}.{minor}"
    if build > 0 or revision > 0:
        result += f".{build}"
    if revision > 0:
        result += f".{revision}"
    return result


@dataclass
class UpdateInfo:
    current: tuple
    latest: tuple
    tag: str
    changelog: str  # Markdown
    url: str


def fetch_releases(timeout: float = 15) -> list:
    request = urllib.request.Request(RELEASES_API, headers={
        "User-Agent": "CustomRP",
        "Accept": "application/vnd.github+json",
    })
    with urllib.request.urlopen(request, timeout=timeout) as response:
        return json.load(response)


def _release_notes(body: str) -> str:
    # Release bodies start with "Changes:" and end with descriptions of the .exe and .zip downloads
    lines = (body or "").replace("\r\n", "\n").split("\n")
    while lines and not lines[-1].strip():
        lines.pop()
    return "\n".join(lines[1:-2]).strip()


def check(current: str = __version__):
    """Returns UpdateInfo if a newer release exists, otherwise None. Network errors propagate."""
    releases = [r for r in fetch_releases() if not r.get("draft") and not r.get("prerelease")]
    if not releases:
        return None

    current_v = parse_version(current)
    latest = releases[0]
    latest_v = parse_version(latest["tag_name"])

    if latest_v <= current_v:
        return None

    changelog = []
    for release in releases:
        try:
            if parse_version(release["tag_name"]) <= current_v:
                break
        except ValueError:
            continue
        changelog.append(f"### {release.get('name') or release['tag_name']}\n\n{_release_notes(release.get('body'))}")

    return UpdateInfo(current_v, latest_v, latest["tag_name"], "\n\n".join(changelog),
                      latest.get("html_url") or RELEASES_PAGE)
