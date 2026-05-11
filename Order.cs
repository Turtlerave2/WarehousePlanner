using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehousePlanner
{
    internal class Order
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }

        public int DeliveryWindowStart { get; set; }
    }
}
