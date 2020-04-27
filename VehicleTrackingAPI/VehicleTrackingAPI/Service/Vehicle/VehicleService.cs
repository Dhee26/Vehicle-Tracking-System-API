using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using VehicleTrackingAPI.Data.Repository.Vehicle;
using VehicleTrackingAPI.DTO;
using VehicleTrackingAPI.DTO.Google_Geocode;

namespace VehicleTrackingAPI.Service.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehiclePositionDTO> GetCurrentVehiclePositionAsync(int userID, int deviceID)
        {
            VehiclePositionDTO result = null;
            try
            {
                // Check a device or user cannot update the position of another vehicle
                int CheckUserDevicePosition = await _vehicleRepository.CheckUserWithVehicleDevice(userID, deviceID);
                if (CheckUserDevicePosition > 0)
                {
                    result = await _vehicleRepository.GetCurrentVehiclePositionAsync(userID, deviceID);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        public async Task<List<VehiclePositionDTO>> GetVehiclePositionRangeAsync(int userID, int deviceID, DateTime startDate, DateTime endDate)
        {
            List<VehiclePositionDTO> result = null;
            try
            {
                // Check a device or user cannot update the position of another vehicle
                int CheckUserDevicePosition = await _vehicleRepository.CheckUserWithVehicleDevice(userID, deviceID);
                if (CheckUserDevicePosition > 0)
                {
                    result = await _vehicleRepository.GetVehiclePositionRangeAsync(userID, deviceID, startDate, endDate);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        public async Task<bool> RecordVehiclePositionAsync(VehiclePositionDTO vehiclePosition)
        {
            // Check a device or user cannot update the position of another vehicle
            int checkUserDevicePosition = await _vehicleRepository.CheckUserWithVehicleDevice(vehiclePosition.UserID, vehiclePosition.DeviceID);
            if (checkUserDevicePosition > 0)
            {
                return await _vehicleRepository.RecordVehiclePositionAsync(vehiclePosition);
            }
            return false;
        }

        public async Task<bool> RegisterVehicleAsync(VehicleDTO vehicle)
        {
            //Check if user exist by Mail id
            int userExist = await _vehicleRepository.CheckUserExist(vehicle.User.EmailID);
            if (userExist == 0)
            {
                // Check if VehicleRegisterNumber already exist
                int vehicleExist = await _vehicleRepository.CheckVehicleExist(vehicle.VehicleRegisterNumber);
                if (vehicleExist == 0)
                {
                    return await _vehicleRepository.RegisterVehicleAsync(vehicle);
                }
            }
            return false;
        }

        // Bonus :) 
        public async Task<string> GetMatchingLocality(string position)
        {
            string YOUR_API_KEY = ""; // test with real api key code
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?latlng={0}&key={1}", Uri.EscapeDataString(position), YOUR_API_KEY);

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();    

            // Deserialise the data 
            Rootobject deserialized = JsonConvert.DeserializeObject<Rootobject>(result);
            // results.address_components.types.locality = get long_name with comma separated
            return deserialized.results.Select(x => new Address_Components
            {
                long_name = x.address_components.FirstOrDefault().long_name
            }).Where(x => x.types.ToString() == "sublocality").ToString();
        }
    }
}