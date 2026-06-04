# Warehouse Route Optimizer (Ballyboughal Edition)

### 🌐 [View Live Portfolio Preview](https://turtlerave2.github.io)

## 🖥️ System Dashboard & Interface Preview
![Warehouse Optimizer Automated Test Suite](images/Screenshot%202026-06-04%20203331.png)

## 📌 The Problem & Goal
**The Problem:** Manual routing takes way too long and depends completely on head knowledge and continuous checking of google maps. 
**The Goal:** Eliminate manual scheduling entirely, reduce driver "zigzagging", ensure efficient van loading, and guarantee the driver is safely back at the Ballyboughal warehouse before the strict **8:00 PM driver cutoff**.

---

## 🛠️ Interactive Console App & Datasets
The application now features a dynamic interactive menu allowing you to test the routing engine across different logistical scenarios:
1. **Narrow Dataset:** Cleanly aligned stops following major transit corridors.
2. **Wide Dataset:** Scattered stops requiring broad geographic sweeping.
3. **Sample Orders List:** A full mixed-batch delivery run.
4. **🧪 Native Test Suite:** Run instant, zero-dependency automated logic checks. (self-made for now)

---

## 🚀 Smart Routing (How it Works)
The engine automatically analyzes the geographical spread of the coordinates and selects the optimal sorting algorithm:

* **Straight Line Mode (South-to-North):** Used for routes that naturally line up along a main corridor (like the M7/M8). It recognizes when stops are linear and sorts them sequentially by latitude.
* **Wide Circle Mode (Quadrant-based):** Used when deliveries are scattered across the country. It calculates angles relative to Ballyboughal and "sweeps" through regions like a clock hand to prevent overlapping tracks.
* **Work-Backwards Logic:** Both modes ensure the driver hits the furthest stop first while the van is full, working their way home towards the warehouse as the vehicle gets lighter and easier to handle.
* **Automatic Outlier Isolation:** The system automatically calculates the average longitude cluster. If a rogue delivery is detected too far out from the pack, it flags and isolates it to prevent ruining the route's efficiency.

---

## ⏱️ Realistic Workday Logic
The time-tracking math mirrors the precise constraints of a real delivery driver's day:
* **Unloading Buffer:** Automatically injects a fixed 30-minute window into every single stop to account for unloading furniture and customer handovers.
* **Home Commute Simulation:** Accurately maps the final transit leg, calculating the distance and time required to travel from the final customer stop back to the base hub.
* **Hard Cutoff Warnings:** Simulates the journey at an average operational speed of 80 km/h. If cumulative driving and unloading times breach 20:00 (8 PM), the terminal flags a warning suggesting the run be postponed.

---

## 🧠 Technical Challenges Solved
* **Negative Coordinate Math:** Fixed a critical calculation bug where Western hemisphere longitudes (which are negative values in Ireland) were confusing the basic distance logic. Integrated a standard mathematical normalization structure to keep distance calculations precise.
* **Missing/Broken Data Defense:** Added an initialization check to intercept missing coordinates (towns showing up as `0,0`). The engine drops these broken points cleanly so they cannot corrupt the sorting matrix.
* **Compiler Ghost Caches:** Cleaned out conflicting multi-framework build files (.NET 8 vs .NET 9 artifacts) to consolidate the app down to a streamlined, high-performance native .NET 8 runtime project structure.
* **Geo Coordinate:** Resolved a critical distance matrix calculation bug where Western Hemisphere Prime Meridian variances inverted basic absolute tracking logic. Implemented a geometric normalization structure to ensure absolute calculation results.

---

## 🛠️ Production Stack & Tools
* **Core Language:** C# (Strongly Typed Object-Oriented Architecture)
* **Runtime Ecosystem:** .NET 8 SDK (Native Execution Engine)
* **Mathematical Framework:** Haversine Spherical Trigonometry Formula
* **Version Control & Docs:** Git, GitHub, and Markdown Engine
* **Data Integration:** JSON Data Parsing Profiles / Operational Datasets

---

## 🧪 Testing Harness
To avoid configuration friction and heavy external frameworks, this project utilizes a custom, lightweight automated test suite (`RunTests.cs`). Triggered directly via the console menu, it runs instant assertions verifying:
1. High-precision spherical distance logic using the Haversine formula.
2. Coordinate clustering accuracy and precise outlier isolation thresholds.