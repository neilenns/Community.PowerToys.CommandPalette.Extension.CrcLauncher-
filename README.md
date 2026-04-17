# Community.PowerToys.CommandPalette.Extension.CrcLauncher

A [PowerToys Command Palette](https://learn.microsoft.com/windows/powertoys/command-palette/overview) extension for launching CRC profiles, matching the behavior of the existing PowerToys Run CRC launcher.

## How it works

- The extension reads CRC profile JSON files from:
  - `HKCU\Software\CRC\Install_Dir\Profiles` (preferred), or
  - `%LOCALAPPDATA%\CRC\Profiles` (fallback).
- It shows your profiles in Command Palette under **CRC launcher**.
- Typing in the page search box filters profiles by name.
- Selecting a profile launches:
  - `%LOCALAPPDATA%\CRC\Application\crc.exe --profile=<profileId>`

## Installation

1. Download the latest release zip from the [releases page](https://github.com/neilenns/Community.PowerToys.CommandPalette.Extension.CrcLauncher-/releases/latest).
2. Extract the zip.
3. Install it in PowerToys Command Palette (recommended via Command Palette extension install flow), or copy the extracted files into your local Command Palette extensions folder.
4. Restart PowerToys.

## Development

### Build locally

```powershell
dotnet restore .\src\Community.PowerToys.CommandPalette.Extension.CrcLauncher.csproj
dotnet build .\src\Community.PowerToys.CommandPalette.Extension.CrcLauncher.csproj -c Release -p:Platform=x64
```

### GitHub automation

This repo includes workflows in `.github/workflows`:

- **CI build** (`ci.yml`): runs on pull requests and validates x64 + arm64 builds/publish steps.
- **Release application** (`release.yml`): runs when a GitHub release is created, publishes x64 + arm64, zips outputs, and uploads release assets automatically.
