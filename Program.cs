using WarehousePlanner;

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
    new Order { FirstName = "Test1", County = "Cork", City = "Mitchelstown" }, 
    new Order { FirstName = "Test2", County = "Tipperary", City = "Cashel" },
    new Order { FirstName = "Test3", County = "Laois", City = "Portlaoise" },
    new Order { FirstName = "Test4", County = "Kildare", City = "Naas" },
    new Order { FirstName = "Test5", County = "Dublin", City = "Tallaght" }    
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

//(Longitude Spread) for straight line or circle method of delivery
Console.WriteLine("--- RAW COORDINATE CHECK ---");
foreach (var o in narrowOrders)
{
    Console.WriteLine($"Order for {o.City}: Lat {o.Lat:F4}, Lon {o.Lon:F4}");
}
Console.WriteLine("----------------------------");

// look at list only 
var allLons = narrowOrders.Select(o => o.Lon).ToList();
double minLon = allLons.Min();
double maxLon = allLons.Max();

// Subtracting gives the true geographic gap
double lonSpread = Math.Abs(maxLon - minLon);

Console.WriteLine($"DEBUG: Calculated MinLon: {minLon:F4}");
Console.WriteLine($"DEBUG: Calculated MaxLon: {maxLon:F4}");
Console.WriteLine($"DEBUG: Final Longitude Spread: {lonSpread:F4}");


// See the actual spread in the console (how big gap)
Console.WriteLine($"DEBUG: Longitude Spread is {lonSpread:F4}");

IEnumerable<Order> optimizedRoute;

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
        .ThenBy(o => GetDistance(o.Lat, o.Lon));
}



// Print it out to the Console
Console.WriteLine("--- BALLYBOUGHAL WAREHOUSE: OPTIMIZED ROUTE ---");
Console.WriteLine("-----------------------------------------------");

var routeList = optimizedRoute.ToList();
for (int i = 0; i < routeList.Count; i++)
{
    var stop = routeList[i];
    Console.WriteLine($"{i + 1}. {stop.City} ({stop.County})");
}

Console.WriteLine("-----------------------------------------------");
Console.WriteLine("Route Complete. Return to Base.");
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