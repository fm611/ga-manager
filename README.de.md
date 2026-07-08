<div align="center">
  <img src="branding/logo.png" alt="Gruppenadressen Manager" width="420" />

  <p>Ein Windows-Desktop-Tool zum Anlegen und Pflegen von KNX-Gruppenadressen –<br/>
  ETS-kompatibler CSV-Import/Export, Vorlagen, Undo/Redo.</p>

  <p><a href="README.md">🇬🇧 English version</a></p>
</div>

<br/>

![Screenshot](docs/screenshot.png)

<details>
<summary>Screenshot auf Englisch</summary>

![Screenshot (Englisch)](docs/screenshot-en.png)

</details>

## Über das Projekt

**Gruppenadressen Manager** verwaltet die 3-Level-Gruppenadressstruktur eines KNX-Projekts
(Hauptgruppe / Mittelgruppe / Untergruppe) außerhalb von ETS – z. B. um eine Adressstruktur
vorab zu planen und anschließend als CSV in ETS zu importieren. Die Oberfläche ist auf Deutsch
und Englisch verfügbar (umschaltbar über das Sprachmenü im Menüband) und wird automatisch
gespeichert und beim nächsten Start wiederhergestellt.

- **Hauptgruppen & Gruppenadressen** in einer Tabelle pro Hauptgruppe pflegen, mit Zellen
  einfügen/löschen, Mehrfachauswahl, Copy/Paste und Suche
- **Gruppen** (frei definierbare Tags quer über Hauptgruppen hinweg, z. B. nach Raum) zur
  Filterung des Grids
- **Gruppen-Templates**: wiederkehrende Adress-Layouts (z. B. Schalten/Dimmen/Status) als
  Vorlage anlegen und in beliebige Hauptgruppen einfügen

  ![Template Manager](docs/template-manager.png)

- **Undo/Redo** für alle Änderungen
- **CSV-Import/-Export** im ETS-Main/Middle/Sub-Schema, inkl. automatischer
  Zeichenkodierungs-Erkennung (UTF-8 / Windows-1252) für Dateien aus ETS/Excel
- **Projektdateien** (`.gaproj`) mit Speichern/Speichern unter, Zuletzt-verwendet-Liste und
  Änderungs-Tracking

## Technik

Die App ist ein WPF-Fenster, das eine React-Oberfläche über WebView2 einbettet:

| Projekt | Beschreibung |
|---|---|
| `GAManager.Desktop` | .NET-10-WPF-Host: Fenster, native Datei-Dialoge (Öffnen/Speichern/Export/Import), zuletzt verwendete Dateien. Bettet den React-Build als statische Dateien ein und lädt sie per WebView2 – keine Internetverbindung nötig. |
| `GAManager.Web` | React 19 + TypeScript + Vite + Fluent UI. Enthält das komplette Domänenmodell, die Undo/Redo-Logik sowie CSV-Import/-Export. Kommuniziert mit dem WPF-Host über eine schmale `postMessage`-Bridge ([`src/host/wpfBridge.ts`](src/GAManager.Web/src/host/wpfBridge.ts)). |

Die React-App lässt sich auch eigenständig im Browser starten (`npm run dev`) – dann greifen
Fallbacks (Datei-Download/-Upload) anstelle der WPF-Bridge.

## Entwicklung

Voraussetzungen: [.NET 10 SDK](https://dotnet.microsoft.com/download), [Node.js](https://nodejs.org/) (npm).

```bash
# React-App im Dev-Modus (Hot Reload, eigenständig im Browser unter http://localhost:5173)
cd src/GAManager.Web
npm install
npm run dev

# Desktop-App bauen & starten (baut die React-App automatisch mit und bettet sie ein)
dotnet build src/GAManager.Desktop/GAManager.Desktop.csproj
dotnet run --project src/GAManager.Desktop
```

Typecheck / Lint der Web-App:

```bash
cd src/GAManager.Web
npx tsc -b
npm run lint
```

## Tests

```bash
cd src/GAManager.Web
npm run test
```

## Release-Build

Die App wird als self-contained Single-File-EXE veröffentlicht (keine .NET-Runtime auf dem
Zielrechner nötig):

```bash
dotnet publish src/GAManager.Desktop/GAManager.Desktop.csproj -c Release -r win-x64 --self-contained true -p:PublishProfile=FolderProfile
```

Ergebnis liegt unter `src/GAManager.Desktop/bin/Release/net10.0-windows7.0/publish/win-x64/`.

## Projektstruktur

```
src/
  GAManager.Desktop/  WPF-Host (Fenster, Datei-I/O, WebView2)
  GAManager.Web/      React-Oberfläche (Domänenmodell, UI, CSV-Import/-Export)
branding/             Logo & Icon (Quelldateien)
docs/                 Screenshots & sonstige Dokumentationsmedien
.github/workflows/    CI (Build & Tests)
```
