using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO.Google_Geocode
{
    public class Result
    {
        public Address_Components[] address_components { get; set; }
    }
}