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

        private double CalculateKm(Order d1, Order d2)
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

        public void CalculateRouteSchedule(List<Order> route, double startHour = 8.0)
        {
            double currentHour = startHour;
            double driverSpeedKmH = 80.0;

            for (int i = 0; i < route.Count; i++)
            {
                //Calculate distance Lat/Lon 
                double distance = (i == 0) ? 0 : CalculateKm(route[i - 1], route[i]);

                //drive time 
                double driveTime = distance / driverSpeedKmH;
                currentHour += driveTime;

                Console.WriteLine($"Arriving at {route[i].City} ({route[i].FirstName}) at {FormatTime(currentHour)}");

                //Add the unloading time
                currentHour += 0.5;

                //Check against the 8 PM hard cutoff
                if (currentHour > 20.0)
                {
                    Console.WriteLine("Warning: This delivery exceeds the 8 PM driver cutoff!");
                }
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
