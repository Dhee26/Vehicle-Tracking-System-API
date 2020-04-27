using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.Models
{
    public class VehicleDevice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeviceID { get; set; }

        public string DeviceName { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}