using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleTrackingAPI.DTO;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Data.Repository.Vehicle
{
    public interface IVehicleRepository
    {
        Task<bool> RegisterVehicleAsync(VehicleDTO vehicle);
        Task<bool> RecordVehiclePositionAsync(VehiclePositionDTO vehiclePosition);
        Task<VehiclePositionDTO> GetCurrentVehiclePositionAsync(int userID, int deviceId);
        Task<List<VehiclePositionDTO>> GetVehiclePositionRangeAsync(int userID, int deviceId, DateTime startDate, DateTime endDate);
       
        Task<int> CheckVehicleExist(string vehicleRegisterNumber);
        Task<int> CheckUserExist(string emailId);
        Task<int> CheckUserWithVehicleDevice(int userID, int deviceId);
    }
}
