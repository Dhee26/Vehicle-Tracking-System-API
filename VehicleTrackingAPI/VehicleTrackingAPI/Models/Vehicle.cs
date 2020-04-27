using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.Models
{
    public class Vehicle
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "Please Vehicle Name"), MaxLength(30)]
        public string VehicleName { get; set; }

        [Required(ErrorMessage = "Please Vehicle Register Number"), MaxLength(20)]
        public string VehicleRegisterNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateTimeStamp { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<VehicleDevice> VehicleDevices { get; set; }
    }    
}