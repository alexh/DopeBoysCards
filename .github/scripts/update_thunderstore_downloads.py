#!/usr/bin/env python3
"""Fetch Thunderstore download count and update README + stats JSON."""

from __future__ import annotations

import json
import os
import re
import sys
import urllib.request
from urllib.error import HTTPError, URLError
from datetime import datetime, timezone
from pathlib import Path

SOURCE_URL = "https://thunderstore.io/c/rounds/p/Phalex/DopeBoys/"
README_PATH = Path("README.md")
STATS_PATH = Path("stats/thunderstore-downloads.json")
START_MARKER = "<!-- thunderstore-downloads:start -->"
END_MARKER = "<!-- thunderstore-downloads:end -->"
PATTERNS = (
    re.compile(r"Total\s*downloads[^0-9]{0,40}([0-9][0-9,]*)", re.IGNORECASE | re.DOTALL),
    re.compile(r'"total_downloads"\s*:\s*([0-9][0-9,]*)', re.IGNORECASE),
    re.compile(r'"totalDownloads"\s*:\s*([0-9][0-9,]*)', re.IGNORECASE),
    re.compile(r'"download_count"\s*:\s*([0-9][0-9,]*)', re.IGNORECASE),
)


def fetch_html() -> str:
    local_html_path = os.environ.get("THUNDERSTORE_HTML_PATH")
    if local_html_path:
        return Path(local_html_path).read_text(encoding="utf-8")

    request = urllib.request.Request(
        SOURCE_URL,
        headers={
            "User-Agent": (
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) "
                "AppleWebKit/537.36 (KHTML, like Gecko) "
                "Chrome/122.0.0.0 Safari/537.36"
            ),
            "Accept": "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
            "Accept-Language": "en-US,en;q=0.9",
            "Cache-Control": "no-cache",
            "Pragma": "no-cache",
        },
    )

    try:
        with urllib.request.urlopen(request, timeout=30) as response:
            return response.read().decode("utf-8", errors="replace")
    except HTTPError as exc:
        raise RuntimeError(f"HTTP {exc.code} while fetching Thunderstore page") from exc
    except URLError as exc:
        raise RuntimeError(f"Network error while fetching Thunderstore page: {exc.reason}") from exc


def parse_download_count(html: str) -> int:
    if "Just a moment..." in html or "cf-chl" in html:
        raise RuntimeError("Thunderstore returned an anti-bot challenge page; unable to parse downloads")

    for pattern in PATTERNS:
        match = pattern.search(html)
        if match:
            raw = match.group(1).replace(",", "")
            return int(raw)

    raise RuntimeError("Could not parse Thunderstore download count from page HTML")


def build_stats_payload(count: int, today_utc: str) -> dict[str, object]:
    return {
        "count": count,
        "count_formatted": f"{count:,}",
        "source_url": SOURCE_URL,
        "updated_at_utc": today_utc,
    }


def update_readme(readme: str, count: int, today_utc: str) -> str:
    block = (
        f"{START_MARKER}\n"
        f"Thunderstore downloads: **{count:,}** (updated {today_utc} UTC)\n"
        f"{END_MARKER}"
    )

    if START_MARKER not in readme or END_MARKER not in readme:
        raise RuntimeError("README markers for Thunderstore downloads were not found")

    pattern = re.compile(
        rf"{re.escape(START_MARKER)}.*?{re.escape(END_MARKER)}",
        re.DOTALL,
    )
    return pattern.sub(block, readme, count=1)


def write_if_changed(path: Path, content: str) -> bool:
    old = path.read_text(encoding="utf-8") if path.exists() else ""
    if old == content:
        return False
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(content, encoding="utf-8")
    return True


def main() -> int:
    try:
        html = fetch_html()
        count = parse_download_count(html)
        today_utc = datetime.now(timezone.utc).strftime("%Y-%m-%d")

        updated_files = []

        readme_old = README_PATH.read_text(encoding="utf-8")
        readme_new = update_readme(readme_old, count, today_utc)
        if write_if_changed(README_PATH, readme_new):
            updated_files.append(str(README_PATH))

        stats_payload = build_stats_payload(count, today_utc)
        stats_content = json.dumps(stats_payload, indent=2) + "\n"
        if write_if_changed(STATS_PATH, stats_content):
            updated_files.append(str(STATS_PATH))

        print(f"download_count={count}")
        print(f"download_count_formatted={count:,}")
        print(f"updated_at_utc={today_utc}")
        if updated_files:
            print("updated_files=" + ",".join(updated_files))
        else:
            print("updated_files=")
        return 0
    except Exception as exc:  # noqa: BLE001
        print(f"ERROR: {exc}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
