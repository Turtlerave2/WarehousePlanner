using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehousePlanner
{
    internal class LocationService
    {
        //base of operations location
        public const double BaseLat = 53.520;
        public const double BaseLon = -6.268;


        public static Dictionary<string, (double Lat, double Lon)> CountyCoords = new()
        {
    { "Louth", (53.882, -6.459) },
    { "Meath", (53.605, -6.650) },
    { "Wicklow", (52.980, -6.041) },
    { "Wexford", (52.336, -6.460) },
    { "Waterford", (52.190, -7.150) },
    { "Cork", (51.898, -8.471) },
    { "Kerry", (52.154, -9.566) },
    { "Limerick", (52.663, -8.626) },
    { "Clare", (52.900, -9.000) },
    { "Galway", (53.270, -9.056) },
    { "Mayo", (53.850, -9.300) },
    { "Sligo", (54.271, -8.476) },
    { "Donegal", (54.654, -8.109) },
    { "Roscommon", (53.750, -8.250) },
    { "Westmeath", (53.533, -7.466) },
    { "Cavan", (53.989, -7.360) },
    { "Monaghan", (54.249, -6.968) },
    { "Tipperary", (52.640, -7.920) },
    { "Kilkenny", (52.654, -7.244) },
    { "Carlow", (52.718, -6.817) },
    { "Laois", (53.000, -7.333) },
    { "Offaly", (53.233, -7.666) },
    { "Kildare", (53.200, -6.750) },
    { "Longford", (53.733, -7.800) },
    { "Leitrim", (54.125, -8.000) },
    { "Dublin", (53.280, -6.360) },
        };

    }
}
