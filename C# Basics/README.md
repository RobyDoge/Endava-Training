# C# Practical Exercise — Ride-Share Simulator

A console application designed to practice core C# concepts: **inheritance**, **polymorphism**, **interfaces**, **collections**, **LINQ**, **events**, and **JSON persistence**.

---

## 📘 Overview

You will build a simplified **ride-share simulation system**. The goal is to model a small ecosystem of drivers, passengers, vehicles, and trips, while implementing clean object-oriented principles and event-driven design.

---

## 🧩 Core Entities

### `Person` (base)
- `Id`
- `Name`

### `Driver : Person`
- `Vehicle`
- `Rating`
- `Status` (`Available`, `OnTrip`, `Offline`)

### `Passenger : Person`
- `PaymentMethod`
- `LoyaltyPoints`

### `Vehicle` (base)
- `Plate`
- `Capacity`
- `BaseFareKm`
- `CalcCost(distanceKm)` — **virtual**

#### Derived Vehicles
- **`Car`**: includes a flat fee for the first 2 km  
- **`Scooter`**: cheaper per km, no surge pricing  
- **`LuxuryCar`**: higher multiplier and minimum fare

### `Trip`
- `Id`
- `PassengerId`
- `DriverId`
- `DistanceKm`
- `Cost`
- `Status` (`Requested`, `Accepted`, `Completed`, `Cancelled`)

### `PriceCaluator`
 - `CalculateCost(distance,driver,passenger)`
---

## ⚙️ Behavior and Features

### Command-Line Interface
Implement commands such as:
add-driver <name> <vehicleType> <plate> <capacity>
add-passenger <name> <payment>
request-trip <passengerId> <from> <to>
accept-trip <driverId> <tripId>
complete-trip <tripId> rating:1-5

cancel-trip <tripId>
list drivers|passengers|trips [filters]
save <file.json>
load <file.json>
help
exit


### Matching Logic
- Select the first **available driver** whose vehicle capacity ≥ requested seats (default 1).
- If multiple, prefer:
  1. Highest rating

### Pricing
1. Base = `Vehicle.CalcCost(distanceKm)`
2. Apply pricing rules in sequence:
   - `SurgeRule`: 18:00–21:00 × 1.5  
   - `WeatherRule`: adds fixed fee if `isRainy = true`  
   - `LoyaltyRule`: every 100 points → 5% discount, then deduct points used

---

## 📡 Events

Implement and log the following events:
- `TripRequested`
- `TripAccepted`
- `TripCompleted`
- `TripCancelled`
- `DriverBecameIdle`

All events should:
- Print to console  
- Be stored in an in-memory event log  

---

## 💾 Persistence

- Save and load all data (drivers, passengers, trips) as **JSON**.
- Validate file format and handle errors with **custom exceptions**.


## 🧠 Polymorphism Demonstration

Call `CalcCost` using `Vehicle` references only.  
Each subclass must yield a different total cost, confirming dynamic dispatch.

---
