using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ForeignKey("UserRole")]
        public int RoleID { get; set; }
        public virtual UserRole UserRole { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}