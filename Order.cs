using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public string _eircode { get; set; }
        public string Address { get; set; }

        public string Eircode
        {
            get => _eircode;
            set
            {
                string cleaned = value?.Replace(" ", "").ToUpper() ?? "";

                if (IsValidEircode(cleaned))
                {
                    _eircode = cleaned.Insert(3, " ");
                }
                else
                {
                    _eircode = "INVALID";
                }
            }
        }


        //For back-end system info
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsOutlier { get; set; } = false;

        // Time it takes to unload furniture (10 mins default)
        public double ServiceTimeHours { get; set; } = 10.0 / 60.0;


        public static bool IsValidEircode(string eircode)
        {
            if (string.IsNullOrWhiteSpace(eircode)) return false;

            //Strip spaces
            string testStr = eircode.Replace(" ", "").ToUpper();

            string pattern = @"^(A[0-9]{2}|D6W|[CDEFHKNPRTVWXY][0-9]{2})[ACDEFHKNPRTVWXY0-9]{4}$";

            return Regex.IsMatch(testStr, pattern) && testStr.Length == 7;
        }
    }
}
