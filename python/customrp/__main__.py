"""Command line entry point: python -m customrp [options] [preset file path]"""

import argparse
import os
import sys

HELP_EPILOG = ("Option(s) and file path(s) can be included in any order. "
               "Including more than one file path will result in the last valid one being used.")


def main(argv=None) -> int:
    parser = argparse.ArgumentParser(prog="customrp", description="Custom Discord Rich Presence Manager",
                                     epilog=HELP_EPILOG, add_help=False)
    parser.add_argument("-?", "-h", "--help", action="help", help="shows this help text")
    parser.add_argument("-2", "--second-instance", action="store_true", help="open as second instance")
    parser.add_argument("-s", "--silent-import", action="store_true", help="silent preset import")
    parser.add_argument("-v", "--verbose", action="store_true", help="also log to the console")
    parser.add_argument("files", nargs="*", help="preset file(s) to import")
    args = parser.parse_args(argv)

    preset = None
    for path in args.files:
        if os.path.isfile(path):
            preset = os.path.abspath(path)

    # Absolute imports, so this file also works as a script entry point (e.g. for PyInstaller)
    from customrp import applog
    applog.setup(verbose=args.verbose)

    from customrp.app import run
    return run(preset_file=preset, second_instance=args.second_instance, silent=args.silent_import)


if __name__ == "__main__":
    sys.exit(main())
