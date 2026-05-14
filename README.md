Project Title: Warehouse Route Optimizer (Ballyboughal Edition).

The Problem: Manual routing takes "too long" and depends on "head knowledge" of the map.

The Goal: Reduce driver "zigzagging" and ensure efficient van loading and remove need of manual work and make sure the driver is back at the warehouse before 8pm cutoff.

### Smart Routing (How it works)
The app now automatically chooses the best way to sort your stops:

-Straight Line Mode: Used for routes that follow a main road (like the M7/M8). It recognizes when stops are in a line and sorts them so the driver hits the furthest point first.

--Wide Circle Mode: Used when deliveries are scattered across the country. It "sweeps" through regions (like a clock hand) to make sure the driver isn't crossing back and forth over the same roads.

--Work-Backwards Logic: Both modes ensure the driver hits the furthest stop first while the van is full and works its way home towards ballyboughal as it gets lighter.

### Realistic workday logic (New)
I have updated the math to reflect how a real day of the delivery driver looks like:

--A buffer: the app auto adds 30 minute to every stop to account for the unloading of the furniture and setting up for the next stop.

--Home commute: Calculates the last stretch home. Driving from the last customer stop back to the warehouse (base). 

--Warning: If the total drive and unload time goes past 8pm, the app flags it immediately so the delivery can be postponed.

### Challenges I fixed
-The Negative Number Math: Fixed a bug where Irish map coordinates (which are negative) were confusing the computer's distance math.

-The Missing Data Fix: Added a check to catch any town that was missing coordinates (showing as 0,0). The app now ignores these "broken" points so they don't ruin the rest of the route.

### Tested Scenarios
-The South Run: Successfully sorted Cork → Tipperary → Laois → Dublin in a clean line.

-The National Run: Successfully sorted a wide trip from Wicklow across to Galway and Kerry in a logical circle.

-A drive simulation: simple testing using 80Km/h to see the rough estimate of the entire journey and arrival time back to base.