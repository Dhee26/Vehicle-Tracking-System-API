using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingAPI.DTO;

namespace VehicleTrackingAPI.Service.Vehicle
{
    public interface IVehicleService
    {
        Task<bool> RegisterVehicleAsync(VehicleDTO vehicle);
        Task<bool> RecordVehiclePositionAsync(VehiclePositionDTO vehiclePosition);
        Task<VehiclePositionDTO> GetCurrentVehiclePositionAsync(int userID, int deviceID);
        Task<List<VehiclePositionDTO>> GetVehiclePositionRangeAsync(int userID, int deviceID, DateTime startDate, DateTime endDate);

        Task<string> GetMatchingLocality(string position);
    }
}
