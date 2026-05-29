using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehousePlanner
{
    public static class TestData
    {
        //basic standard tests

        // Scenario A ---> The standard narrow line route down the M7/M8 give or take 
        public static List<Order> GetSouthRun()
        {
            return new List<Order>
            {
                new Order { FirstName = "Customer_Cork", City = "Mitchelstown", Lat = 52.268, Lon = -8.284 },
                new Order { FirstName = "Customer_Tipp", City = "Cashel", Lat = 52.516, Lon = -7.889 },
                new Order { FirstName = "Customer_Laois", City = "Portlaoise", Lat = 53.034, Lon = -7.300 },
                new Order { FirstName = "Customer_Kildare", City = "Naas", Lat = 53.218, Lon = -6.664 },
                new Order { FirstName = "Customer_Dublin", City = "Tallaght", Lat = 53.288, Lon = -6.368 }
            };
        }

        // Scenario B: The massive cross the country scattered run (circle)
        public static List<Order> GetNationalWideRun()
        {
            return new List<Order>
            {
                new Order { FirstName = "Cust_Wicklow", City = "Arklow", Lat = 52.793, Lon = -6.152 },
                new Order { FirstName = "Cust_Galway", City = "Oranmore", Lat = 53.271, Lon = -8.931 },
                new Order { FirstName = "Cust_Kerry", City = "Tralee", Lat = 52.268, Lon = -9.699 }
            };
        }

        // Scenario C: Testing edge-cases (Missing data /Broken coordinates)
        public static List<Order> GetBrokenDataRun()
        {
            return new List<Order>
            {
                new Order { FirstName = "Valid_Kildare", City = "Naas", Lat = 53.218, Lon = -6.664 },
                new Order { FirstName = "Buggy_Data_1", City = "UnknownTown", Lat = 0, Lon = 0 }, //bad one
                new Order { FirstName = "Valid_Dublin", City = "Swords", Lat = 53.459, Lon = -6.218 }
            };
        }

    }
}
