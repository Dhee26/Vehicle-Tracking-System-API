using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO.Google_Geocode
{
    public class Address_Components
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
}