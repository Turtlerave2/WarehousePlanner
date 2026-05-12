Project Title: Warehouse Route Optimizer (Ballyboughal Edition).

The Problem: Manual routing takes 'too long' and depends on "head knowledge" of the map.

The Goal: Reduce driver "zigzagging" and ensure efficient van loading and remove need of manual work and help automate deliveries. 

### Smart Routing (How it works)
The app now automatically chooses the best way to sort your stops:

-**Straight Line Mode: Used for routes that follow a main road (like the M7/M8). It detects when the stops are in a narrow line and sorts them from the furthest point back toward the warehouse.

-**Wide Circle Mode: Used when deliveries are scattered across the country. It "sweeps" through regions to make sure the driver isn't crossing back and forth over the same roads.

Work-Backwards Logic: Both modes ensure the driver hits the furthest stop first while the van is full and works their way home as it gets lighter.

### Challenges I fixed
- **The Negative Number Math: Fixed a bug where Irish map coordinates (which are negative) were confusing the computer's distance math.

The Missing Data Fix: Added a check to catch any town that was missing coordinates (showing as 0,0). The app now ignores these "broken" points so they don't ruin the rest of the route.

### Tested Scenarios
-The South Run: Successfully sorted Cork → Tipperary → Laois → Dublin in a clean line.

-The National Run: Successfully sorted a wide trip from Wicklow across to Galway and Kerry in a logical circle.