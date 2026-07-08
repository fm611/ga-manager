# Changelog

Alle nennenswerten Änderungen an diesem Projekt werden hier dokumentiert.
Das Format orientiert sich an [Keep a Changelog](https://keepachangelog.com/de/1.1.0/).

## [Unreleased]

## [1.0.1] - 2026-07-08

### Added

- Footer mit aktueller Versionsnummer und einem Info-Icon, das zum GitHub-Repository verlinkt.

### Removed

- `GAManager.Core` und das zugehörige Testprojekt `GAManager.Core.Tests` entfernt: Das
  Domänenmodell lebt vollständig in `GAManager.Web` (TypeScript/Zod), `GAManager.Core` wurde
  von `GAManager.UI.WPF` nicht mehr referenziert.

### Changed

- Projekte und Namespaces von `GroupAddress.*` auf `GAManager.*` umbenannt (`GAManager.UI.WPF`,
  `GAManager.Web`, `GAManager.sln`), passend zum umbenannten GitHub-Repo `ga-manager`. Der
  Produktname "Gruppenadressen Manager" in der UI bleibt unverändert.
- `GAManager.UI.WPF` weiter in `GAManager.Desktop` umbenannt (Namespace, XAML `x:Class`,
  Projekt-/Ordnername), da der `.UI.WPF`-Suffix ohne `GAManager.Core` nicht mehr nötig war.

## [1.0.0] - 2026-07-08

Erstes Release.

### Changed

- Projektstruktur an gängige GitHub-Konventionen angepasst: `GroupAddress.Core`, `GroupAddress.UI.WPF`
  und `GroupAddress.Web` liegen jetzt unter `src/`.
- `GroupAddress.Web` wird in der `.sln` als JavaScript-Projekt (`.esproj`) statt als (nicht mehr
  funktionierendem) ASP.NET-Website-Projekt geführt.

### Added

- `tests/GroupAddress.Core.Tests` (xUnit) für `GroupAddress.Core`.
- Vitest-Setup (`npm run test`) für `GroupAddress.Web`.
- `LICENSE` (MIT).
- CI-Workflow (GitHub Actions) für Build und Tests.
