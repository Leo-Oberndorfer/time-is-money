# Calculation Logic (Time Is Money)

## 1. Derived Times

Für jede Fahrt existieren zwei Methoden: CAR und PUBLIC.

- `ArrivalUtc(method) = DepartureUtc + DurationMinutes(method)`
- Wenn `ScheduledArrivalUtc` gesetzt ist:
  - `TimeDiffMinutes(method) = (ArrivalUtc(method) - ScheduledArrivalUtc)` in Minuten
  - Interpretation:
    - negativ = zu früh
    - 0 = pünktlich
    - positiv = zu spät

---

## 2. Point System

Pro Fahrt werden Punkte für CAR und PUBLIC berechnet.

### 2.1 Wenn Scheduled arrival gesetzt ist

**(A) Nähe zur Soll-Ankunft**
- Bestimme die absolute Abweichung:
  - `AbsDiff(method) = abs(TimeDiffMinutes(method))`
- Die Methode mit der kleineren `AbsDiff` bekommt `+1` Punkt.
- Bei Gleichstand bekommt niemand diesen Punkt.

**(B) Zu spät ankommen**
- Wenn `TimeDiffMinutes(method) > 0` (zu spät), gibt es eine Strafe:
  - Für CAR: `-1`
  - Für PUBLIC:
    - wenn `Delayed == True`: `-2`
    - sonst: `-1`

Hinweise:
- Zu früh sein ist keine Strafe.
- Die Delayed-Strafe gilt nur, wenn PUBLIC zu spät ankommt.

### 2.2 Wenn Scheduled arrival NICHT gesetzt ist

- Die Methode mit der kleineren `DurationMinutes` bekommt `+1` Punkt.
- Bei Gleichstand: kein Punkt.

---

## 3. Decision Verdict

Die App bewertet die Entscheidung basierend auf den Punkten:

- `ChosenBetter` wenn `Points(chosen) > Points(other)`
- `ChosenWorse` wenn `Points(chosen) < Points(other)`
- sonst `NoDifference`

---

## 4. Fuel Cost (CAR Basis)

Für Geld-Kennzahlen wird die CAR-Seite als Kostenbasis verwendet.

- `FuelLiters = (DistanceKm / 100) * AverageConsumptionLPer100Km`
- `FuelCostEur = FuelLiters * FuelPricePerLiterEur`
- `PeopleCount = AdditionalPassengers + 1`

---

## 5. Money/Time Metric

### 5.1 Wenn ChosenTravel == CAR

Ziel: „Wie viel € pro Minute pro Person wurde bezahlt, um schneller zu sein?“

- `TimeSavedMinutes = PublicDurationMinutes - CarDurationMinutes`

Regeln:
- Wenn `TimeSavedMinutes <= 0`: Kennzahl = `null` (kein Zeitgewinn)
- Sonst:
  - `CostPerMinutePerPerson = FuelCostEur / (TimeSavedMinutes * PeopleCount)`

### 5.2 Wenn ChosenTravel == PUBLIC

Ziel: „Wie viel € pro Minute pro Person wurden gespart, obwohl es länger dauert?“

- `TimeLostMinutes = PublicDurationMinutes - CarDurationMinutes`

Regeln:
- Wenn `TimeLostMinutes <= 0`: Kennzahl = `null` (kein Zeitverlust bzw. PUBLIC ist nicht langsamer)
- Sonst:
  - `MoneySavedPerMinutePerPerson = FuelCostEur / (TimeLostMinutes * PeopleCount)`

---

## 6. Statistics (Aggregation)

Über alle gespeicherten Fahrten:

- `CountCarChosen`: Anzahl Fahrten mit `ChosenTravel == CAR`
- `CountPublicChosen`: Anzahl Fahrten mit `ChosenTravel == PUBLIC`

- `AvgDurationCar`: Durchschnitt `CarDurationMinutes` (über alle Fahrten)
- `AvgDurationPublic`: Durchschnitt `PublicDurationMinutes` (über alle Fahrten)

- `AvgFuelCostCarChosen`: Durchschnitt `FuelCostEur` über Fahrten, wo CAR gewählt wurde

- `TotalSpentEur`:
  - Summe `FuelCostEur` über Fahrten, wo CAR gewählt wurde
- `TotalSavedEur`:
  - Summe `FuelCostEur` über Fahrten, wo PUBLIC gewählt wurde

- `TotalKmCarChosen`:
  - Summe `DistanceKm` über Fahrten, wo CAR gewählt wurde
- `TotalFuelLitersCarChosen`:
  - Summe `FuelLiters` über Fahrten, wo CAR gewählt wurde

- CO₂ (nur CAR gewählt):
  - `TotalCo2Grams = TotalKmCarChosen * 125`
  - Optional zusätzlich: `TotalCo2Kg = TotalCo2Grams / 1000`

- Entscheidungen:
  - `GoodDecisionsCount`: Anzahl Fahrten mit `ChosenBetter`
  - `BadDecisionsCount`: Anzahl Fahrten mit `ChosenWorse`
  - `NoDifference` zählt nicht in gut/schlecht

- `TotalDelaysPublic`:
  - Anzahl Fahrten mit `PublicDelayed == True`

---

## 7. Test Cases (Minimum Set)

### 7.1 Point System with Scheduled arrival

1) **CAR näher an Scheduled arrival**
- Departure: 2026-02-06T08:00:00Z
- Scheduled: 2026-02-06T08:55:00Z
- CAR Duration: 55 (arrives 08:55 diff 0)
- PUBLIC Duration: 70 (arrives 09:10 diff +15)
- Expect:
  - Nähe-Punkt: CAR +1
  - Late-Strafe: PUBLIC -1 (nicht delayed) oder -2 (wenn delayed)
  - Verdict abhängig von ChosenTravel

2) **PUBLIC delayed und zu spät**
- Scheduled gesetzt
- PUBLIC diff +1 und Delayed=True
- Expect: PUBLIC Late-Strafe = -2

3) **Gleichstand bei AbsDiff**
- CAR diff -5, PUBLIC diff +5
- Expect: kein Nähe-Punkt (0 Punkte aus Nähe-Regel)

### 7.2 Point System without Scheduled arrival

4) **Schnellere Methode bekommt +1**
- Scheduled missing
- CAR Duration 40, PUBLIC Duration 65
- Expect: CAR +1, PUBLIC 0

5) **Gleichstand**
- Scheduled missing
- CAR Duration 50, PUBLIC Duration 50
- Expect: beide 0

### 7.3 Money/Time Metric

6) **CAR gewählt, Zeitgewinn vorhanden**
- Distance 35, AvgCons 5.1, FuelPrice 1.54, AddPassengers 1
- FuelLiters = 0.35 * 5.1 = 1.785
- FuelCost = 1.785 * 1.54 = 2.7489
- TimeSaved = 10, People = 2
- Expect: CostPerMinutePerPerson = 2.7489 / (10*2) = 0.137445

7) **CAR gewählt, kein Zeitgewinn**
- TimeSaved <= 0
- Expect: Kennzahl null

8) **PUBLIC gewählt, Zeitverlust vorhanden**
- gleicher FuelCost wie oben
- TimeLost = 20, People = 2
- Expect: MoneySavedPerMinutePerPerson = 2.7489 / (20*2) = 0.0687225
