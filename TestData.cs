using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehousePlanner
{
    public static class TestData
    {
        public static void ExecuteAll()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine(" RUNNING WAREHOUSE PLANNER TEST SUITE ");
            Console.WriteLine("========================================");
            Console.ResetColor();

            int passed = 0;
            int failed = 0;

            // ------------------------------------------------------------
            // TEST 1: Distance Calculation Verification
            // ------------------------------------------------------------
            try
            {
                //test the distance Ballyboughal base and Naas
                Order baseStation = new Order { Lat = 53.518, Lon = -6.265 };
                Order naasStop = new Order { Lat = 53.218, Lon = -6.664 };

                // invoke the methods
                RouteCalculation calculator = new RouteCalculation();

                double distance = calculator.CalculateKm(baseStation, naasStop);

                // Real world distance is roughly 43-44 km
                if (distance < 40 || distance > 46)
                {
                    throw new Exception($"Distance math seems incorrect. Expected ~43.5km, but got {distance:F2}km.");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" TEST 1 PASSED: Distance calculation is highly accurate ({distance:F2} km).");
                passed++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" TEST 1 FAILED: {ex.Message}");
                failed++;
            }

            // ------------------------------------------------------------
            // TEST 2: Outlier Flagging Logic
            // ------------------------------------------------------------
            try
            {
                RouteCalculation calculator = new RouteCalculation();

                // Setup a cluster near Dublin and one outlier far out West 
                List<Delivery> testDeliveries = new List<Delivery>
                {
                    new Delivery { Address = "Dublin Stop 1", Longitude = -6.25, IsOutlier = false },
                    new Delivery { Address = "Dublin Stop 2", Longitude = -6.21, IsOutlier = false },
                    new Delivery { Address = "Galway Outlier", Longitude = -9.05, IsOutlier = false }
                };

                var evaluated = calculator.IdentifyOutliers(testDeliveries, threshold: 1.0);

                if (!testDeliveries[2].IsOutlier)
                    throw new Exception("Galway stop was not flagged as an outlier, but it is far west.");

                if (testDeliveries[0].IsOutlier)
                    throw new Exception("Normal cluster stop was accidentally flagged as an outlier.");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" TEST 2 PASSED: Outlier detection caught the rogue coordinate perfectly.");
                passed++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" TEST 2 FAILED: {ex.Message}");
                failed++;
            }

            // ------------------------------------------------------------
            // SUMMARY
            // ------------------------------------------------------------
            Console.ResetColor();
            Console.WriteLine("========================================");
            Console.WriteLine($" SUMMARY: {passed} Passed | {failed} Failed");
            Console.WriteLine("========================================\n");
        }

    }
}
