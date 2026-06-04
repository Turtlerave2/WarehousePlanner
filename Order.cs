using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehousePlanner
{
    public class Order
    {
        //customer info - based on checkout 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }


        //For back-end system info
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsOutlier { get; set; } = false;

        // Time it takes to unload furniture (30 mins default)
        public double ServiceTimeHours { get; set; } = 10.0 / 60.0;
    }
}
