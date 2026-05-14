namespace WarehousePlanner
{
    internal static class RouteCalculationHelpers
    {
        public static List<Delivery> IdentifyOutliers(List<Delivery> orders, double threshold = 0.5)
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
    }
}