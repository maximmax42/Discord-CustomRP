"""Daily log files in the same format as the Windows app: logs/YYYY-MM-DD.log, "[HH:MM:SS] LEVEL: message"."""

import logging
from datetime import datetime

from .paths import logs_dir

_LEVELS = {"DEBUG": "TRACE", "INFO": " INFO", "WARNING": " WARN", "ERROR": "ERROR", "CRITICAL": "ERROR"}


class DailyFileHandler(logging.Handler):
    def __init__(self, directory):
        super().__init__()
        self.directory = directory

    def emit(self, record):
        try:
            self.directory.mkdir(parents=True, exist_ok=True)
            now = datetime.now()
            line = f"[{now:%H:%M:%S}] {_LEVELS.get(record.levelname, record.levelname)}: {self.format(record)}\n"
            with open(self.directory / f"{now:%Y-%m-%d}.log", "a", encoding="utf-8") as f:
                f.write(line)
        except Exception:
            self.handleError(record)


def setup(verbose: bool = False):
    root = logging.getLogger("customrp")
    root.setLevel(logging.DEBUG)
    root.addHandler(DailyFileHandler(logs_dir()))

    if verbose:
        console = logging.StreamHandler()
        console.setFormatter(logging.Formatter("%(asctime)s %(levelname)s %(name)s: %(message)s"))
        root.addHandler(console)
