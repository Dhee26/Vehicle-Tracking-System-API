using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO
{
    public class VehiclePositionDTO
    {
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime UpdateLocationTimeStamp { get; set; }

        public virtual LocalityDTO Locality { get; set; }
    }
}