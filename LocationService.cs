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


        public static Dictionary<string, (double Lat, double Lon)> TownCoords = new(StringComparer.OrdinalIgnoreCase)
        {
            // Narrow dataset 
            { "Mitchelstown", (52.266, -8.263) },
            { "Cashel", (52.516, -7.889) },
            { "Portlaoise", (53.031, -7.300) },
            { "Naas", (53.218, -6.664) },
            { "Tallaght", (53.288, -6.368) },

            // Wide dataset
            { "Arklow", (52.793, -6.149) },
            { "Galway City", (53.270, -9.056) },
            { "Tralee", (52.271, -9.699) },
            { "Westport", (53.801, -9.523) },
            { "Navan", (53.653, -6.683) },

            // Sample list 
            { "Mallow", (52.138, -8.642) },
            { "Letterkenny", (54.951, -7.734) },
            { "Cork City", (51.898, -8.471) },
            { "Enniscorthy", (52.502, -6.568) },
            { "Killarney", (52.059, -9.507) },
            { "Castlebar", (53.851, -9.300) },
            { "Roscommon Town", (53.629, -8.189) },
            { "Mullingar", (53.523, -7.345) },
            { "Bray", (53.201, -6.111) },
            { "Dundalk", (54.004, -6.402) }
        };
    }
}
