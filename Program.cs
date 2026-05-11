using WarehousePlanner;

//setting up list of orders examples
var orders = new List<Order> {
    new Order { FirstName = "Mick", County = "Cork", City = "Mallow" },
    new Order { FirstName = "Sarah", County = "Donegal", City = "Letterkenny" },
    new Order { FirstName = "John", County = "Cork", City = "Cork City" },
    new Order { FirstName = "Sarah", County = "Donegal", City = "Letterkenny" },
    new Order { FirstName = "Mick", County = "Wexford", City = "Enniscorthy" },
    new Order { FirstName = "Pat", County = "Kerry", City = "Killarney" },
    new Order { FirstName = "Emma", County = "Mayo", City = "Castlebar" },
    new Order { FirstName = "Liam", County = "Roscommon", City = "Roscommon Town" },
    new Order { FirstName = "Niamh", County = "Westmeath", City = "Mullingar" },
    new Order { FirstName = "Dave", County = "Wicklow", City = "Bray" },
    new Order { FirstName = "Claire", County = "Louth", City = "Dundalk" },
    new Order { FirstName = "Bob", County = "Cork", City = "Mallow" }
};


//assigning coordinates for ze service
foreach (var o in orders)
{
    if (LocationService.CountyCoords.ContainsKey(o.County))
    {
        var coords = LocationService.CountyCoords[o.County];
        o.Lat = coords.Lat;
        o.Lon = coords.Lon;
    }
}

//the circle plan trip thingy
var sortedRoute = orders
    .OrderByDescending(o => GetAngle(o.Lat, o.Lon) / 90)
    .ThenBy(o => GetDistance(o.Lat, o.Lon))
    .ToList();



// 4. Print it out to the Console
Console.WriteLine("--- BALLYBOUGHAL WAREHOUSE: OPTIMIZED ROUTE ---");
Console.WriteLine("-----------------------------------------------");

for (int i = 0; i < sortedRoute.Count; i++)
{
    var stop = sortedRoute[i];
    Console.WriteLine($"{i + 1}. {stop.City} ({stop.County})");
}

Console.WriteLine("-----------------------------------------------");
Console.WriteLine("Route Complete. Return to Base.");
Console.ReadLine(); // Keeps the window open so you can read it!




//--------------------------------------------///---------------------------------------------//
                                      //methods for above

//maths jumbo to help coords
static double GetAngle(double targetLat, double targetLon)
{
    // Math.Atan2 returns the angle in radians between the X-axis and the point 
    double deltaLat = targetLat - LocationService.BaseLat;
    double deltaLon = targetLon - LocationService.BaseLon;

    // Convert to degrees and normalize to 0-360
    double angle = Math.Atan2(deltaLat, deltaLon) * (180 / Math.PI);
    return (angle + 360) % 360;
}

//more maths stuff , good enough for ballpark sorting-> will make better
static double GetDistance(double targetLat, double targetLon)
{
    // Straight line distance (Pythagorean theorem) rough estimate of distance
    return Math.Sqrt(Math.Pow(targetLat - LocationService.BaseLat, 2) +
                     Math.Pow(targetLon - LocationService.BaseLon, 2));
}