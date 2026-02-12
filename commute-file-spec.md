# Commute File Specification

## Background

Eine *Commute File* ist eine Textdatei, die immer nur eine Pendelfahrt enthalten kann.
Eine Fahrt besteht aus einem Header (Zeitpunkt, Ziel, Wahl der Methode) und zwei Blöcken mit Messwerten:
- CAR (Auto)
- PUBLIC (Öffentliche Verkehrsmittel)

## Example (gültig)

```txt
Departure: 2026-02-06T14:30:00Z
Travel: CAR
Destination: Home
Scheduled arrival: 2026-02-06T15:35:00Z
=====
Method: CAR
Additional passengers: 1
Distance: 35
Duration: 35
Average consumption: 5.1
Spent: 1.54
=====
Method: PUBLIC
Duration: 65
Delayed: False
```

## Specifications
# Commute File – Spezifikation

## Allgemeines
Eine **Commute File** beschreibt genau **eine Pendelfahrt**.  
Sie besteht aus:

1. **Header** mit Metadaten zur Fahrt  
2. **Messblock CAR**  
3. **Messblock PUBLIC**

Header und Messblöcke werden jeweils durch eine eigene Zeile mit exakt ```=====``` getrennt.

Andere Trennzeichen sind ungültig.

---

## Aufbau

### 1. Header

#### Pflichtfelder

| Feld                 | Typ        | Beschreibung                         |
|----------------------|------------|-------------------------------------|
| Departure            | ISO-8601   | Startzeitpunkt der Fahrt            |
| Travel               | Enum       | `CAR` oder `PUBLIC`                 |
| Destination          | String     | Zielort (nicht leer)                |

#### Optional

| Feld                 | Typ      | Beschreibung                          |
|----------------------|----------|--------------------------------------|
| Scheduled arrival    | ISO-8601 | Geplante Ankunftszeit                |

#### Regeln

- `Departure` muss ein gültiges ISO-8601-Datum sein  
- Falls `Scheduled arrival` vorhanden ist:  
  - muss es ein gültiges ISO-8601-Datum sein  
  - muss **nach** `Departure` liegen  
- `Travel` darf **nur eine** Methode enthalten (`CAR` oder `PUBLIC`)

---

### 2. CAR-Block

Beginnt nach dem ersten `=====`.

#### Pflichtfelder

| Feld                   | Typ      | Regel                    |
|------------------------|----------|--------------------------|
| Method                 | String   | Muss `CAR` sein          |
| Distance               | Decimal  | > 0                      |
| Duration               | Integer  | > 0                      |
| Average consumption    | Decimal  | > 0                      |
| Spent                  | Decimal  | > 0                      |

#### Optional

| Feld                   | Typ      | Regel                    |
|------------------------|----------|--------------------------|
| Additional passengers  | Integer  | > 0                      |

---

### 3. PUBLIC-Block

Beginnt nach dem zweiten `=====`.

#### Pflichtfelder

| Feld      | Typ     | Regel              |
|-----------|---------|--------------------|
| Method    | String  | Muss `PUBLIC` sein |
| Duration  | Integer | > 0                |
| Delayed   | Boolean | `true` oder `false`|

---

## Validierungsregeln

Eine Datei ist **gültig**, wenn:

- genau **eine Fahrt** enthalten ist  
- Header, CAR-Block und PUBLIC-Block vorhanden sind  
- die Trennlinien exakt `=====` sind  
- alle Pflichtfelder vorhanden sind  
- Datumswerte gültig sind  
- `Departure` vor `Scheduled arrival` liegt (falls vorhanden)  
- numerische Werte positiv sind  
- `Travel` nur eine Methode enthält  
- keine unbekannten Methoden verwendet werden  

---

## Nicht erlaubt

- Mehr als eine Fahrt pro Datei  
- Fehlende oder falsche Trennlinien  
- Negative oder nicht numerische Werte in numerischen Feldern  
- Andere Methoden als `CAR` oder `PUBLIC`  
- Leere `Destination`  
