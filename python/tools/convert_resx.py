"""Generates the Python app's data files from the original C# project.

* customrp/locales/<code>.json - UI strings taken from the .resx files
* customrp/data/people.json   - languages, translators and supporters taken from Utils.cs

Run it again whenever the translations or supporter lists in CustomRPC/ change:

    python tools/convert_resx.py
"""

import json
import re
import sys
import xml.etree.ElementTree as ET
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
CSHARP = ROOT / "CustomRPC"
OUT = Path(__file__).resolve().parents[1] / "customrp"

# Forms whose localized text is used by the Python UI
FORMS = ["MainForm", "About", "UpdatePrompt", "PipeSelector", "ErrorReportViewer"]
TEXT_SUFFIXES = (".Text", ".ToolTip", ".ToolTipText", ".BalloonTipText", ".BalloonTipTitle")


def read_resx(path, only_text):
    strings = {}
    for data in ET.parse(path).getroot().iter("data"):
        name = data.get("name", "")
        value = data.find("value")

        if name == "$this.RightToLeft" and value is not None and value.text == "Yes":
            strings["_rtl"] = True
            continue
        if name.startswith(">>") or data.get("type") or data.get("mimetype") or value is None:
            continue
        if only_text and not name.endswith(TEXT_SUFFIXES):
            continue

        strings[name] = (value.text or "").replace("\r\n", "\n")
    return strings


def convert_locales():
    locales = {}

    for path in sorted(CSHARP.glob("*.resx")):
        parts = path.name.split(".")
        form = parts[0]

        if form not in FORMS and form != "Strings":
            continue

        code = parts[1].lower() if len(parts) == 3 else "en"
        strings = read_resx(path, only_text=form != "Strings")
        target = locales.setdefault(code, {})

        for key, value in strings.items():
            target[key if key.startswith("_") else f"{form}.{key}"] = value

    out_dir = OUT / "locales"
    out_dir.mkdir(parents=True, exist_ok=True)

    for code, strings in locales.items():
        with open(out_dir / f"{code}.json", "w", encoding="utf-8", newline="\n") as f:
            json.dump(dict(sorted(strings.items())), f, ensure_ascii=False, indent=1)
            f.write("\n")

    print(f"Wrote {len(locales)} locale files to {out_dir}")


# --- Utils.cs parsing -------------------------------------------------------

STRING = r'"((?:[^"\\]|\\.)*)"'


def cs_string(s):
    return s.encode("utf-8").decode("unicode_escape").encode("latin-1").decode("utf-8") if "\\" in s else s


def fields(block):
    """Extracts `Field = "value"` and `Field: "value"` pairs from a C# object initializer."""
    return {k: cs_string(v) for k, v in re.findall(r"(\w+)\s*[=:]\s*" + STRING, block)}


def balanced(text, start):
    """Returns the index right after the brace/paren block that opens at `start`."""
    pairs = {"{": "}", "(": ")"}
    stack = []
    i = start
    in_str = False
    while i < len(text):
        c = text[i]
        if in_str:
            if c == "\\":
                i += 1
            elif c == '"':
                in_str = False
        elif c == '"':
            in_str = True
        elif c in pairs:
            stack.append(pairs[c])
        elif stack and c == stack[-1]:
            stack.pop()
            if not stack:
                return i + 1
        i += 1
    raise ValueError("Unbalanced block")


def strip_comments(text):
    # Strings are matched first so that URLs inside them survive
    return re.sub(r'("(?:[^"\\\n]|\\.)*")|//[^\n]*', lambda m: m.group(1) or "", text)


def convert_people():
    src = strip_comments((CSHARP / "Utils.cs").read_text(encoding="utf-8-sig"))

    # Named persons declared as static fields
    named = {}
    for name, body in re.findall(r"static Person (\w+) = new Person\s*(\{[^}]*\})", src):
        named[name] = fields(body)

    def parse_people(block):
        people = []
        pos = 0
        for m in re.finditer(r"new Person\s*\{|\b([A-Za-z_]\w*)\s*,", block):
            if m.start() < pos:
                continue
            if m.group(0).startswith("new Person"):
                end = balanced(block, m.end() - 1)
                people.append(fields(block[m.start():end]))
                pos = end
            elif m.group(1) in named:
                people.append(dict(named[m.group(1)]))
        return people

    languages = []
    for m in re.finditer(r"new Language\s*\{", src):
        end = balanced(src, m.end() - 1)
        block = src[m.end():end - 1]
        tl_match = re.search(r"Translators\s*=\s*new Person\[\]\s*\{", block)
        head = block[:tl_match.start()] if tl_match else block
        lang = fields(head)
        lang["Translators"] = []
        if tl_match:
            tl_end = balanced(block, tl_match.end() - 1)
            lang["Translators"] = parse_people(block[tl_match.end():tl_end - 1] + ",")
        languages.append(lang)

    progress = dict(re.findall(r'LanguageProgress\["([^"]+)"\]\s*=\s*' + STRING, src))

    supporters = []
    for m in re.finditer(r"new Supporter\s*([{(])", src):
        end = balanced(src, m.end() - 1)
        block = src[m.end():end - 1]
        supporter = {}
        ref = re.match(r"\s*(\w+)\s*,", block)
        if m.group(1) == "(" and ref and ref.group(1) in named:
            supporter.update(named[ref.group(1)])
        supporter.update(fields(block))
        supporters.append(supporter)

    non_monetary = []
    for m in re.finditer(r"new NonMonetarySupporter\s*\{", src):
        end = balanced(src, m.end() - 1)
        non_monetary.append(fields(src[m.end():end - 1]))

    data = {
        "languages": languages,
        "progress": progress,
        "supporters": supporters,
        "nonMonetarySupporters": non_monetary,
    }

    out_dir = OUT / "data"
    out_dir.mkdir(parents=True, exist_ok=True)
    with open(out_dir / "people.json", "w", encoding="utf-8", newline="\n") as f:
        json.dump(data, f, ensure_ascii=False, indent=1)
        f.write("\n")

    print(f"Wrote {len(languages)} languages, {len(supporters)} supporters, "
          f"{len(non_monetary)} non-monetary supporters to {out_dir / 'people.json'}")


if __name__ == "__main__":
    if not CSHARP.is_dir():
        sys.exit(f"Can't find the C# project at {CSHARP}")
    convert_locales()
    convert_people()
