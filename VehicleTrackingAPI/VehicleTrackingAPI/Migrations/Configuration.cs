namespace VehicleTrackingAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VehicleTrackingAPI.Data.Security;
    using VehicleTrackingAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VehicleTrackingAPI.Data.VehicleTrackingAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VehicleTrackingAPI.Data.VehicleTrackingAPIContext context)
        {
            //SeedUserRole(context);           

            //SeedUser(context);
        }

        private void SeedUser(VehicleTrackingAPI.Data.VehicleTrackingAPIContext context)
        {
            // Adding User Admin credientials
            context.Users.Add(new User() { RoleID = 1, UserName = "AdminUser", EmailID = "Adminuser@vts.com", Password = PasswordHash.HashSHA1("dheerajVTS") });
        }

        private void SeedUserRole(VehicleTrackingAPI.Data.VehicleTrackingAPIContext context)
        {
            // Adding UserRole
            context.UserRoles.Add(new UserRole() { RoleName = "Admin" });
            context.UserRoles.Add(new UserRole() { RoleName = "User" });           
        }
    }
}
