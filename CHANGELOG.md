# Changelog

Alle nennenswerten Änderungen an diesem Projekt werden hier dokumentiert.
Das Format orientiert sich an [Keep a Changelog](https://keepachangelog.com/de/1.1.0/).

## [Unreleased]

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
