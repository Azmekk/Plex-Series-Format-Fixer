# Plex Series Format Fixer

## Overview

Plex Series Format Fixer is an application designed to traverse a specified folder and automatically format episode/season names according to predefined patterns. The app searches for naming patterns specified in a `patterns.json` file (which you can edit) and replaces them with a standardized format: `SXXEXX`.

## Getting Started
1. Download the [latest release](https://github.com/Azmekk/Plex-Series-Format-Fixer/releases/latest) targeting your platform.
2. (Optional) Edit the `patterns.json` file if the ep/season pattern you are trying to target is not included.
3. Copy the path to the folder holding your episodes.
4. Run the application and follow the prompts.

## Supported Patterns:

| Pattern                          | Description                                      | Example Input        | Output      |
|----------------------------------|--------------------------------------------------|----------------------|-------------|
| `S(\d{2})-(\d{2})`               | Matches "S01-02"                                | `S01-02`             | `S01E02`    |
| `S(\d{2})[._]E(\d{2})`           | Matches "S01.E02" or "S01_E02"                  | `S01.E02`            | `S01E02`    |
| `Season (\d) Episode (\d)`       | Matches "Season 1 Episode 2"                     | `Season 1 Episode 2` | `S01E02`    |
| `S(\d)E(\d)`                     | Matches "S1E2"                                  | `S1E2`               | `S01E02`    |
| `S(\d)\s-\sE(\d{2})`             | Matches "S1 - E02"                              | `S1 - E02`           | `S01E02`    |
| `S(\d)\s-\s(\d{2})`              | Matches "S1 - 02"                               | `S1 - 02`            | `S01E02`    |
| `S(\d{2})\sE(\d{2})`             | Matches "S01 E02"                               | `S01 E02`            | `S01E02`    |

## Asking me to include patterns
If you think there is a common pattern that I haven't included, please submit an [issue](https://github.com/Azmekk/Plex-Series-Format-Fixer/issues/new/choose) and I'll do my best to add it. 

## Adding patterns Yourself
Simply open the `patterns.json` file and add the Regex pattern you want to include. Something like ChatGPT can be useful in generating those. 

#### Example
1. You have a file named `Series Name Season 1 Episode 1.mkv`
2. You need to replace `Season 1 Episode 1` with `S01E01` (The app already includes this but this is simply an example)
3. Find out the regex for the pattern (In this case it's `S(\d{2})[._]E(\d{2})`)
4. Append that regex to the property called `patterns` in `patterns.json`
5. Run the app.
