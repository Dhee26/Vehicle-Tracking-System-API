using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VehicleTrackingAPI.Data.Security;
using VehicleTrackingAPI.Data.UnitOfWork;
using VehicleTrackingAPI.DTO;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Data.Repository.Vehicle
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly VehicleTrackingAPIContext _dbContext;
        public VehicleRepository(IUnitOfWork UnitOfWork, VehicleTrackingAPIContext dbContext)
        {
            _UnitOfWork = UnitOfWork;
            _dbContext = dbContext;
        }

        public async Task<bool> RegisterVehicleAsync(VehicleDTO vehicle)
        {
            if (vehicle != null)
            {
                var objUser = new VehicleTrackingAPI.Models.User()
                {
                    UserName = vehicle.User.UserName,
                    EmailID = vehicle.User.EmailID,
                    Password = PasswordHash.HashSHA1(vehicle.User.Password),
                    RoleID = 2 // User Role 2
                };
                _UnitOfWork.UserRepository.Add(objUser);

                var objVehicle = new VehicleTrackingAPI.Models.Vehicle()
                {
                    UserID = objUser.UserID,
                    VehicleName = vehicle.VehicleName,
                    VehicleRegisterNumber = vehicle.VehicleRegisterNumber,
                    UpdateTimeStamp = DateTime.Now
                };
                _UnitOfWork.VehicleRepository.Add(objVehicle);

                var objDevice = new VehicleDevice()
                {
                    VehicleID = objVehicle.VehicleID,
                    DeviceName = vehicle.VehicleDevice.DeviceName
                };
                _UnitOfWork.VehicleDeviceRepository.Add(objDevice);

                var result = await _UnitOfWork.CommitAsync();
                if (result > 0)
                    return true;
            }

            return false;
        }

        public async Task<bool> RecordVehiclePositionAsync(VehiclePositionDTO vehiclePosition)
        {
            if (vehiclePosition != null)
            {
                var objVehiclePosition = new Location()
                {
                    DeviceID = vehiclePosition.DeviceID,
                    Latitude = vehiclePosition.Latitude,
                    Longitude = vehiclePosition.Longitude,
                    UpdateLocationTimeStamp = vehiclePosition.UpdateLocationTimeStamp
                };

                _UnitOfWork.LocationRepository.Add(objVehiclePosition);
                var result = await _UnitOfWork.CommitAsync();
                if (result > 0)
                    return true;
            }

            return false;
        }

        public async Task<VehiclePositionDTO> GetCurrentVehiclePositionAsync(int userID, int deviceId)
        {
            var queryObj = await _dbContext.Vehicles
                       .Join(
                           _dbContext.VehicleDevices,
                           v => v.VehicleID,
                           vd => vd.VehicleID,
                           (v, vd) => new { VEHICLE = v, DEVICE = vd })
                       .Join(
                           _dbContext.Locations,
                           vdd => vdd.DEVICE.DeviceID,
                           loc => loc.DeviceID,
                           (vdd, loc) => new { vdd.VEHICLE, vdd.DEVICE, LOCATION = loc })
                       .Select(s => new
                       {
                           UserID = s.VEHICLE.UserID,
                           DeviceID = s.DEVICE.DeviceID,
                           Latitude = s.LOCATION.Latitude,
                           Longitude = s.LOCATION.Longitude,
                           UpdateLocationTimeStamp = s.LOCATION.UpdateLocationTimeStamp
                       }
                       )
                       .Where(w => w.UserID == userID && w.DeviceID == deviceId)
                       .OrderByDescending(o => o.UpdateLocationTimeStamp).Take(1).FirstOrDefaultAsync();

            var result = new VehiclePositionDTO()
            {
                UserID = queryObj.UserID,
                DeviceID = queryObj.DeviceID,
                Latitude = queryObj.Latitude,
                Longitude = queryObj.Longitude,
                UpdateLocationTimeStamp = queryObj.UpdateLocationTimeStamp
            };

            return result;
        }

        public async Task<List<VehiclePositionDTO>> GetVehiclePositionRangeAsync(int userID, int deviceID, DateTime startDate, DateTime endDate)
        {
            var queryObj = await _dbContext.Vehicles
                     .Join(
                         _dbContext.VehicleDevices,
                         v => v.VehicleID,
                         vd => vd.VehicleID,
                         (v, vd) => new { VEHICLE = v, DEVICE = vd })
                     .Join(
                         _dbContext.Locations,
                         vdd => vdd.DEVICE.DeviceID,
                         loc => loc.DeviceID,
                         (vdd, loc) => new { vdd.VEHICLE, vdd.DEVICE, LOCATION = loc })
                     .Select(s => new
                     {
                         UserID = s.VEHICLE.UserID,
                         DeviceID = s.DEVICE.DeviceID,
                         Latitude = s.LOCATION.Latitude,
                         Longitude = s.LOCATION.Longitude,
                         UpdateLocationTimeStamp = s.LOCATION.UpdateLocationTimeStamp
                     }
                     )
                     .Where(
                       w => w.UserID == userID
                       && w.DeviceID == deviceID
                       && w.UpdateLocationTimeStamp >= startDate
                       && w.UpdateLocationTimeStamp <= endDate)
                     .OrderByDescending(o => o.UpdateLocationTimeStamp)
                     .ToListAsync();

            return queryObj.Select(x => new VehiclePositionDTO
            {
                UserID = x.UserID,
                DeviceID = x.DeviceID,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                UpdateLocationTimeStamp = x.UpdateLocationTimeStamp
            }).ToList();
        }

        #region Validation Methods
        public async Task<int> CheckVehicleExist(string vehicleRegisterNumber)
        {
            return await _dbContext.Vehicles.CountAsync(x => x.VehicleRegisterNumber == vehicleRegisterNumber);
        }
        public async Task<int> CheckUserExist(string emailId)
        {
            return await _dbContext.Users.CountAsync(x => x.EmailID == emailId);
        }
        public async Task<int> CheckUserWithVehicleDevice(int userID, int deviceId)
        {
            return await _dbContext.Vehicles
                                   .Join(_dbContext.VehicleDevices,
                                      vehicle => vehicle.VehicleID,
                                      device => device.VehicleID,
                                      (vehicle, device) => new { VEHICLE = vehicle, DEVICE = device })
                                   .Where(vehicleAndDevice => vehicleAndDevice.VEHICLE.UserID == userID
                                    && vehicleAndDevice.DEVICE.DeviceID == deviceId).CountAsync();
        }
        #endregion
    }
}