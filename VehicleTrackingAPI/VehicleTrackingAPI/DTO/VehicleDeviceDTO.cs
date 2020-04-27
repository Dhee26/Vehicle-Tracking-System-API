using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO
{
    public class VehicleDeviceDTO
    {
        public int DeviceID { get; set; }

        public string DeviceName { get; set; }
    }
}