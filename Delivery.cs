using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehousePlanner
{
    public class Delivery
    {

        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //for delivery location to sort
        public bool IsOutlier { get; set; } = false;

        // Time it takes to unload furniture (30 mins default)
        public double ServiceTimeHours { get; set; } = 0.5;
    }

    }
