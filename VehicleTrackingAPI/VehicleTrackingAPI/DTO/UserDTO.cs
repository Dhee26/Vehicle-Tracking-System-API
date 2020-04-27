using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleTrackingAPI.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}