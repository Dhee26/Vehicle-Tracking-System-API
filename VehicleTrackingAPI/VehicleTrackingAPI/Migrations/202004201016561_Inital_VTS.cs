namespace VehicleTrackingAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital_VTS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpdateLocationTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeviceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.VehicleDevices", t => t.DeviceID, cascadeDelete: true)
                .Index(t => t.DeviceID);
            
            CreateTable(
                "dbo.VehicleDevices",
                c => new
                    {
                        DeviceID = c.Int(nullable: false, identity: true),
                        DeviceName = c.String(),
                        VehicleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceID)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .Index(t => t.VehicleID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        VehicleName = c.String(nullable: false, maxLength: 30),
                        VehicleRegisterNumber = c.String(nullable: false, maxLength: 20),
                        UpdateTimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        EmailID = c.String(),
                        Password = c.String(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.UserRoles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "DeviceID", "dbo.VehicleDevices");
            DropForeignKey("dbo.VehicleDevices", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleID", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Vehicles", new[] { "UserID" });
            DropIndex("dbo.VehicleDevices", new[] { "VehicleID" });
            DropIndex("dbo.Locations", new[] { "DeviceID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Vehicles");
            DropTable("dbo.VehicleDevices");
            DropTable("dbo.Locations");
        }
    }
}
