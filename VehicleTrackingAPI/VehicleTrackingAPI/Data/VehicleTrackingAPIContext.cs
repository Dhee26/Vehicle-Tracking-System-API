using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Data
{
    public class VehicleTrackingAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public VehicleTrackingAPIContext() : base("name=VehicleTrackingAPIContext")
        {

        }

        public System.Data.Entity.DbSet<UserRole> UserRoles { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<Vehicle> Vehicles { get; set; }
        public System.Data.Entity.DbSet<VehicleDevice> VehicleDevices { get; set; }
        public System.Data.Entity.DbSet<Location> Locations { get; set; }
    }
}
