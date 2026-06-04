using WarehousePlanner;

RouteCalculation calculator = new RouteCalculation();

// =========================================================================
// DATASETS
// =========================================================================
var orderssList = new List<Order> {
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

var narrowOrdersList = new List<Order> {
    new Order { FirstName = "Test1", County = "Cork", City = "Mitchelstown", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test2", County = "Tipperary", City = "Cashel", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test3", County = "Laois", City = "Portlaoise", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test4", County = "Kildare", City = "Naas", ServiceTimeHours = 0.5 },
    new Order { FirstName = "Test5", County = "Dublin", City = "Tallaght", ServiceTimeHours = 0.5 }
};

var wideOrdersList = new List<Order> {
    new Order { FirstName = "TestA", County = "Wicklow", City = "Arklow" },
    new Order { FirstName = "TestB", County = "Galway", City = "Galway City" },
    new Order { FirstName = "TestC", County = "Kerry", City = "Tralee" },
    new Order { FirstName = "TestD", County = "Mayo", City = "Westport" },
    new Order { FirstName = "TestE", County = "Meath", City = "Navan" }
};


// =========================================================================
// INTERACTIVE MENU LOOP
// =========================================================================
while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("=================================================");
    Console.WriteLine("      WAREHOUSE PLANNER LOGISTICS ROUTER         ");
    Console.WriteLine("=================================================");
    Console.ResetColor();
    Console.WriteLine("1. Run Optimization: Narrow Dataset");
    Console.WriteLine("2. Run Optimization: Wide Dataset");
    Console.WriteLine("3. Run Optimization: Sample Orders List");
    Console.WriteLine("4. Run Automated Logic Tests");
    Console.WriteLine("5. Exit Application");
    Console.WriteLine("-------------------------------------------------");
    Console.Write("Select an option (1-5): ");

    string choice = Console.ReadLine();
    List<Order> selectedOrders = null;

    if (choice == "5") break;

    switch (choice)
    {
        case "1":
            selectedOrders = narrowOrdersList;
            Console.WriteLine("\nLoading Narrow Dataset...");
            break;
        case "2":
            selectedOrders = wideOrdersList;
            Console.WriteLine("\nLoading Wide Dataset...");
            break;
        case "3":
            selectedOrders = orderssList;
            Console.WriteLine("\nLoading Sample Orders Dataset...");
            break;
        case "4":
            // Instantly executes your internal assertions
            TestData.ExecuteAll();
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
            continue;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid option! Press any key to retry...");
            Console.ResetColor();
            Console.ReadKey();
            continue;
    }

    // =========================================================================
    // CORE OPTIMIZATION ENGINE RUN
    // =========================================================================

    // Assign coordinates to whatever list was chosen
    foreach (var o in selectedOrders)
    {
        //coords specific town/city first
        if (LocationService.TownCoords.ContainsKey(o.City))
        {
            var coords = LocationService.TownCoords[o.City];
            o.Lat = coords.Lat;
            o.Lon = coords.Lon;
        }
        //If town isn't tracked, use County point
        else if (LocationService.CountyCoords.ContainsKey(o.County))
        {
            var coords = LocationService.CountyCoords[o.County];
            o.Lat = coords.Lat;
            o.Lon = coords.Lon;
        }
    }

    // Identify Outliers 
    double avgLon = selectedOrders.Average(o => o.Lon);
    double threshold = 2.5;
    var validRoute = selectedOrders.Where(o => Math.Abs(o.Lon - avgLon) <= threshold).ToList();
    var outliers = selectedOrders.Where(o => Math.Abs(o.Lon - avgLon) > threshold).ToList();

    IEnumerable<Order> optimizedRoute;

    // Protect against an empty valid list sequence crashing Max/Min math
    if (validRoute.Count == 0)
    {
        Console.WriteLine("Warning: No valid orders found inside cluster limits.");
        Console.ReadKey();
        continue;
    }

    double lonSpread = Math.Abs(validRoute.Max(o => o.Lon) - validRoute.Min(o => o.Lon));

    // Process route modes
    if (lonSpread < 2.5)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[Mode Detected: Straight Line (South-to-North)]");
        Console.ResetColor();
        optimizedRoute = validRoute.OrderBy(o => o.Lat).ToList();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[Mode Detected: Circular (Quadrant-based)]");
        Console.ResetColor();
        optimizedRoute = validRoute
            .OrderByDescending(o => Math.Round(GetAngle(o.Lat, o.Lon) / 90))
            .ThenBy(o => GetDistance(o.Lat, o.Lon))
            .ToList();
    }

    // Call schedule algorithm handler
    Console.WriteLine("-------------------------------------------------");
    calculator.CalculateRouteSchedule(optimizedRoute.ToList(), startHour: 7.5);

    // Print out outliers if any exist
    if (outliers.Any())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n--- POSTPONED ORDERS (OUTLIERS) ---");
        Console.ResetColor();
        foreach (var o in outliers)
        {
            Console.WriteLine($"Save for later: {o.City} ({o.County}) - Lon Gap: {Math.Abs(o.Lon - avgLon):F2}");
        }
    }

    Console.WriteLine("\nProcessing Complete. Press any key to return to menu.");
    Console.ReadKey();
}
    

        // =========================================================================
        // MATHEMATICAL HELPERS
        // =========================================================================
        static double GetAngle(double targetLat, double targetLon)
{
    double deltaLat = targetLat - LocationService.BaseLat;
    double deltaLon = targetLon - LocationService.BaseLon;
    double angle = Math.Atan2(deltaLat, deltaLon) * (180 / Math.PI);
    return (angle + 360) % 360;
}

static double GetDistance(double targetLat, double targetLon)
{
    return Math.Sqrt(Math.Pow(targetLat - LocationService.BaseLat, 2) +
                     Math.Pow(targetLon - LocationService.BaseLon, 2));
}