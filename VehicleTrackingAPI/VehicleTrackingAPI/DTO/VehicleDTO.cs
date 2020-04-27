using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO
{
    public class VehicleDTO
    {        
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "Please Vehicle Name"), MaxLength(30)]
        public string VehicleName { get; set; }

        [Required(ErrorMessage = "Please Vehicle Register Number"), MaxLength(20)]
        public string VehicleRegisterNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateTimeStamp { get; set; }


        public virtual UserRoleDTO UserRole { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual VehicleDeviceDTO VehicleDevice { get; set; }
        public virtual LocationDTO Location { get; set; }
    }    
}