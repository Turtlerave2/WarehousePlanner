using WarehousePlanner;

RouteCalculation calculator = new RouteCalculation();

//setting up list of narrowOrders examples
var Orderss = new List<Order> {
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

var narrowOrders = new List<Order> {
    new Order { FirstName = "Test1", County = "Cork", City = "Mitchelstown", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test2", County = "Tipperary", City = "Cashel", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test3", County = "Laois", City = "Portlaoise", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test4", County = "Kildare", City = "Naas", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test5", County = "Dublin", City = "Tallaght", ServiceTimeHours = 0.5 }
};

var wideOrders = new List<Order> {
    new Order { FirstName = "TestA", County = "Wicklow", City = "Arklow" },    
    new Order { FirstName = "TestB", County = "Galway", City = "Galway City" }, 
    new Order { FirstName = "TestC", County = "Kerry", City = "Tralee" },     
    new Order { FirstName = "TestD", County = "Mayo", City = "Westport" },   
    new Order { FirstName = "TestE", County = "Meath", City = "Navan" }      
};

//assigning coordinates for ze service
foreach (var o in narrowOrders)
{
    if (LocationService.CountyCoords.ContainsKey(o.County))
    {
        var coords = LocationService.CountyCoords[o.County];
        o.Lat = coords.Lat;
        o.Lon = coords.Lon;
    }
}

// 2. Identify Outliers 
double avgLon = narrowOrders.Average(o => o.Lon);
double threshold = 2.5; // Adjust based on gap
var validRoute = narrowOrders.Where(o => Math.Abs(o.Lon - avgLon) <= threshold).ToList();
var outliers = narrowOrders.Where(o => Math.Abs(o.Lon - avgLon) > threshold).ToList();


// look at list only 
var allLons = narrowOrders.Select(o => o.Lon).ToList();
double minLon = allLons.Min();
double maxLon = allLons.Max();


IEnumerable<Order> optimizedRoute;
double lonSpread = Math.Abs(validRoute.Max(o => o.Lon) - validRoute.Min(o => o.Lon));

//check whether using a straight line delivery or circle delivery methods, and how to organise them
if (lonSpread < 2.5)
{
    Console.WriteLine("Mode Detected: Straight Line (South-to-North)");
    optimizedRoute = narrowOrders.OrderBy(o => o.Lat).ToList();
}
else
{
    Console.WriteLine("Mode Detected: Circular (Quadrant-based)");
    optimizedRoute = narrowOrders
        .OrderByDescending(o => Math.Round(GetAngle(o.Lat, o.Lon) / 90))
        .ThenBy(o => GetDistance(o.Lat, o.Lon))
        .ToList();
}

Console.WriteLine("\n--- BALLYBOUGHAL WAREHOUSE: DAILY SCHEDULE ---");
Console.WriteLine($"Driver Speed: 80 km/h | Return Cutoff: 20:00\n"); 

calculator.CalculateRouteSchedule(optimizedRoute.ToList(), startHour: 7.5);
var finalPath = optimizedRoute.ToList();

if (outliers.Any())
{
    Console.WriteLine("\n--- POSTPONED ORDERS (OUTLIERS) ---");
    foreach (var o in outliers)
    {
        Console.WriteLine($"Save for later: {o.City} ({o.County}) - Lon Gap: {Math.Abs(o.Lon - avgLon):F2}");
    }
}

Console.WriteLine("\nOptimization Complete. Press Enter to exit.");
Console.ReadLine();



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