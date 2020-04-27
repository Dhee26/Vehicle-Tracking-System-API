using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO.Google_Geocode
{
    public class Rootobject
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }
}