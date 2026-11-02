# CustomRP for Linux, macOS (and Windows)

A cross-platform port of the Custom Discord Rich Presence Manager, written in Python with Qt (PySide6).
It has the same features and UI as the Windows app, and its `.crp` presets work in both versions.

## Running

You need Python 3.9 or newer and a Discord desktop client (the regular, Flatpak or Snap version, or Vesktop).

```sh
cd python
python3 -m venv .venv
source .venv/bin/activate        # Windows: .venv\Scripts\activate
pip install .
customrp                          # or: python -m customrp
```

Command line options are the same as the Windows app's:

```
customrp [-2|--second-instance] [-s|--silent-import] [-v|--verbose] [preset.crp]
```

If CustomRP is already running, starting it again brings the window up, and passing a preset file imports it
into the running instance.

### Tray icon

The app minimizes to the system tray. On GNOME you need the
[AppIndicator extension](https://extensions.gnome.org/extension/615/appindicator-support/) to see it.
Without a tray, closing the window quits the app instead.

## Differences from the Windows app

| Windows app | This port |
|---|---|
| Settings in .NET `user.config` | `~/.config/customrp/settings.json` (Linux), `~/Library/Application Support/CustomRP/` (macOS), `%LOCALAPPDATA%\CustomRP-Python\` (Windows) |
| Run on Startup via a Start Menu shortcut | XDG autostart `.desktop` file / LaunchAgent / `HKCU\...\Run`. **Off by default.** |
| Downloads and runs the update installer | Shows the changelog and opens the release page |
| AppCenter analytics and crash reports | Removed (AppCenter shut down in 2025); crashes go to `crash.log` next to the settings |
| Changing the language restarts the app | The window is rebuilt in place |
| Uses Lachee's DiscordRPC library | Talks to Discord's IPC socket directly (`customrp/discord_ipc.py`) |

Settings aren't migrated from the Windows app; save a preset there and load it here.

## Translations and supporters

The UI strings in `customrp/locales/` and the translators/supporters list in `customrp/data/people.json` are
generated from the C# project. After updating the `.resx` files or `Utils.cs`, regenerate them:

```sh
python tools/convert_resx.py
```

## Development

```sh
pip install -e ".[test]"
pytest
```

| Module | Purpose |
|---|---|
| `discord_ipc.py` | Discord IPC client (Unix socket / Windows named pipe, one worker thread) |
| `presence.py` | Turns settings into an activity payload: URL handling, image proxying, timestamps |
| `preset.py` | `.crp` files, compatible with .NET's XmlSerializer output |
| `settings.py`, `i18n.py`, `autostart.py`, `updates.py` | What the names say |
| `ui/` | Main window, tray, dialogs, theming |

To ship a standalone binary, [PyInstaller](https://pyinstaller.org) works:
`pyinstaller --windowed --name CustomRP --collect-data customrp -i customrp/assets/favicon.ico customrp/__main__.py`
