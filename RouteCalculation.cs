using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehousePlanner
{
     public class RouteCalculation
    {
        public List<Delivery> IdentifyOutliers(List<Delivery> orders, double threshold = 0.5)
        {
            if (orders.Count == 0) return orders;

            // Calculate average longitude
            double avgLon = orders.Average(d => d.Longitude);

            foreach (var order in orders)
            {
                // If the gap is bigger than threshold, flag it
                if (Math.Abs(order.Longitude - avgLon) > threshold)
                {
                    order.IsOutlier = true;
                    Console.WriteLine($"Flagged Outlier: {order.Address} (Too far from cluster)");
                }
            }
            return orders;
        }

        public double CalculateKm(Order d1, Order d2)
        {
            double R = 6371;
            double dLat = ToRadians(d2.Lat - d1.Lat); 
            double dLon = ToRadians(d2.Lon - d1.Lon); 

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(d1.Lat)) * Math.Cos(ToRadians(d2.Lat)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }


        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public void CalculateRouteSchedule(List<Order> route, double startHour = 6.0)
        {
            //for testing purpose to force base as last "delivery"
            Order warehouseBase = new Order
            {
                City = "Ballyboughal (Warehouse)",
                Lat = 53.518,
                Lon = -6.265
            };

            double currentHour = startHour;
            Order currentPosition = warehouseBase;

            for (int i = 0; i < route.Count; i++)
            {
                //Calculate distance Lat/Lon 
                double distance = CalculateKm(currentPosition, route[i]);

                double driverSpeedKmH = 80.0; 
                string currentCounty = route[i].County.ToLower();

                if (currentCounty == "dublin" || currentCounty == "cork")
                {
                    driverSpeedKmH = 40.0; // city limit speed
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[Traffic Notice: Entering urban zone ({route[i].County}). Dropping speed to 40km/h]");
                    Console.ResetColor();
                }

                //drive time 
                double driveTime = distance / driverSpeedKmH;
                currentHour += driveTime;

                Console.WriteLine($"Arriving at {route[i].City} ({route[i].FirstName}) at {FormatTime(currentHour)}");

                //Add the unloading time
                currentHour += 10.0/60.0;
                currentPosition = route[i];
            }

            double distBack = CalculateKm(currentPosition, warehouseBase);
            double returnSpeed = 80.0;
            double returnDriveTime = distBack / returnSpeed;
            currentHour += returnDriveTime;
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"RETURN TO BASE: Arrived at {warehouseBase.City} at {FormatTime(currentHour)}");

            //Check against the 8 PM hard cutoff
            if (currentHour > 20.0)
            {
                Console.WriteLine("Warning: This delivery exceeds the 8 PM driver cutoff!");
            }
        }

        // Helper to turn decimal into time format
        private string FormatTime(double decimalHours)
        {
            TimeSpan t = TimeSpan.FromHours(decimalHours);
            return t.ToString(@"hh\:mm");
        }

    }
}
