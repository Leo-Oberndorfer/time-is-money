# Time Is Money

## Einleitung

Öffentliche Verkehrsmittel sind umweltfreundlicher. Trotzdem ist es im Alltag manchmal schneller (und teurer), mit dem Auto zu pendeln. In dieser Übung entwickelst du eine App, die Pendelfahrten importiert, speichert und auswertet: Zeit vs. Geld vs. Zuverlässigkeit.

Vereinfachungen und Annahmen:
- Öffentliche Verkehrsmittel sind kostenlos (Ticketkosten werden ignoriert).
- Alle Zeitpunkte sind UTC (ISO 8601 mit `Z`).
- Eine Fahrt enthält immer Vergleichsdaten für **CAR** und **PUBLIC**, auch wenn nur eine Methode gewählt wurde.

Die Regeln zur Berechnung und Bewertung stehen in [calculation-logic.md](./calculation-logic.md).
Das Importformat ist in [commute-file-spec.md](./commute-file-spec.md) beschrieben.
Beispieldateien liegen im Ordner `data/` (korrekt und fehlerhaft).

---

## Funktionale Anforderungen

### US1: Fahrten-Datei hochladen (Import)

Als Benutzer:in möchte ich eine Fahrten-Datei im spezifizierten Format hochladen können, damit meine Pendelfahrten gespeichert und ausgewertet werden.

Akzeptanzkriterien:
- Der Upload akzeptiert Textdateien im Format aus [commute-file-spec.md](./commute-file-spec.md).
- Bei fehlerhaften Dateien werden verständliche Fehlermeldungen ausgegeben.

### US2: Übersicht über alle Fahrten

Als Benutzer:in möchte ich eine Übersicht über alle importierten Fahrten sehen.

Akzeptanzkriterien:
- Sortierung: neueste Fahrt zuerst (z.B. nach `Departure` absteigend).
- Pro Fahrt werden mindestens angezeigt:
  - Departure (UTC)
  - Destination
  - gewählte Methode (CAR/PUBLIC)
  - Dauer CAR und Dauer PUBLIC
  - Ergebnis der Entscheidung (gut/schlecht/kein Unterschied) gemäß Punktesystem
  - die relevante Kennzahl:
    - bei CAR gewählt: „€ pro Minute pro Person“ (Zeitgewinn bezahlt)
    - bei PUBLIC gewählt: „€ pro Minute pro Person“ (Geld gespart trotz Zeitverlust)
- Navigation zur Detailansicht jeder Fahrt.

### US3: Detailansicht einer Fahrt

Als Benutzer:in möchte ich die Details einer einzelnen Fahrt sehen, inklusive aller Eingabedaten und berechneten Werte.

Akzeptanzkriterien:
- Anzeige der Header-Daten (Departure, Destination, Scheduled arrival optional, Chosen travel).
- Anzeige der CAR-Details und PUBLIC-Details.
- Anzeige der berechneten Werte:
  - Arrival-Zeitpunkte beider Methoden
  - Time difference (falls Scheduled arrival gesetzt)
  - Punkte CAR vs PUBLIC
  - Urteil zur gewählten Methode (gut/schlecht/kein Unterschied)
  - Kennzahl „€ pro Minute pro Person“ (je nach gewählter Methode)

### US4: Statistik-Ansicht

Als Benutzer:in möchte ich eine Statistik über alle Fahrten sehen.

Akzeptanzkriterien:
- Anzeige aller Kennzahlen aus [calculation-logic.md](./calculation-logic.md), u.a.:
  - Anzahl CAR/PUBLIC gewählt
  - Durchschnittsdauer CAR/PUBLIC
  - Durchschnittliche FuelCost (bei CAR gewählt)
  - gespartes/ausgegebenes Geld
  - Gesamt-km, Gesamt-Liter, CO₂-Emissionen (125g/km, nur bei CAR gewählt)
  - Anzahl Verspätungen (PUBLIC delayed)
  - Anzahl guter/schlechter Entscheidungen (Punktesystem)

---

## Qualitätsanforderungen

### Startercode

Es muss der bereitgestellte Startercode verwendet werden (Minimal API + EF Core + Angular + Tests).
Der Startercode enthält TODOs, wo Code ergänzt werden muss.

### Zu ergänzender Code (Beispiele)

Je nach Startercode-Struktur sind mindestens folgende Teile zu implementieren:

Backend:
- Parser für das Dateiformat (z.B. `CommuteFileParser.cs`)
- Berechnungslogik (z.B. `CommuteAnalyzer.cs` / `DecisionCalculator.cs`)
- API Endpoints (z.B. `CommuteEndpoints.cs`):
  - Import Endpoint
  - CRUD für Fahrten
  - Statistik Endpoint

Frontend (Angular):
- Import-Komponente (Upload + Ergebnisanzeige)
- Liste aller Fahrten (Filter/Sortierung + Navigation)
- Detailansicht
- Statistikseite

Tests (C#):
- Parser-Tests: gültige/ungültige Dateien, Partial Import, Validierungsfehler
- Logik-Tests: Punktesystem + Kennzahlen (mindestens 5 Unit-Tests)
- Optional: API Integrationstests für Import/Statistics

---
