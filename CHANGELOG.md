# Changelog

All notable changes to this project are documented here. ([🇩🇪 Deutsche Version](CHANGELOG.de.md))
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

## [1.1.0] - 2026-07-08

### Added

- Internationalization (i18n): the entire UI is now available in German and English, switchable
  via a new language menu (globe icon) in the toolbar with country flags (Germany / UK).
- English variant of the logo, shown automatically when the English language is selected.
- English sample-project templates (`defaultTemplates.en.ts`); File → Open → Sample picks the
  matching template set depending on the currently selected language.
- "Save As…" (File menu and Ctrl+Shift+S).
- The selected language is now stored in `config.json` and restored automatically on the next
  launch; the window title for a new, unsaved project ("Neues Projekt" / "New Project") also
  follows the selected language.

### Changed

- `recent.json` renamed to `config.json` and extended with a field for the stored language.

## [1.0.1] - 2026-07-08

### Added

- Footer with the current version number and an info icon linking to the GitHub repository.

### Removed

- Removed `GAManager.Core` and its test project `GAManager.Core.Tests`: the domain model now
  lives entirely in `GAManager.Web` (TypeScript/Zod); `GAManager.Core` was no longer referenced
  by `GAManager.UI.WPF`.

### Changed

- Renamed projects and namespaces from `GroupAddress.*` to `GAManager.*` (`GAManager.UI.WPF`,
  `GAManager.Web`, `GAManager.sln`), matching the renamed GitHub repo `ga-manager`. The product
  name "Gruppenadressen Manager" shown in the UI is unchanged.
- Further renamed `GAManager.UI.WPF` to `GAManager.Desktop` (namespace, XAML `x:Class`,
  project/folder name), since the `.UI.WPF` suffix was no longer needed without
  `GAManager.Core`.

## [1.0.0] - 2026-07-08

First release.

### Changed

- Adjusted the project structure to common GitHub conventions: `GroupAddress.Core`,
  `GroupAddress.UI.WPF` and `GroupAddress.Web` now live under `src/`.
- `GroupAddress.Web` is referenced from the `.sln` as a JavaScript project (`.esproj`) instead
  of a (no longer working) ASP.NET website project.

### Added

- `tests/GroupAddress.Core.Tests` (xUnit) for `GroupAddress.Core`.
- Vitest setup (`npm run test`) for `GroupAddress.Web`.
- `LICENSE` (MIT).
- CI workflow (GitHub Actions) for build and tests.
