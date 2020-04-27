using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO
{
    public class LocationDTO
    {
        public int LocationId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateLocationTimeStamp { get; set; }
    }
}