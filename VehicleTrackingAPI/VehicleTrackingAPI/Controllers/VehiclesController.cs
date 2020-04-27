using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VehicleTrackingAPI.Authentication;
using VehicleTrackingAPI.DTO;
using VehicleTrackingAPI.Models;
using VehicleTrackingAPI.Service.Vehicle;

namespace VehicleTrackingAPI.Controllers
{
    public class VehiclesController : ApiController
    {
        private readonly IVehicleService _vehicleService;
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // Assumption to Register a Vehicle
        /* Below are the api object format
         * {
	            "User": 
    	            { 
    		            "UserName": "Dheeraj G",
    		            "EmailID" : "dhee26@gmail.com"
    	            },
                "VehicleName": "AUDI",
                "VehicleRegisterNumber": "AUDI2020",
                "VehicleDevice" : 
    	            { 
    		            "DeviceName": "DV1" 
    	            }
            }
         */
        // POST: api/Vehicles
        [HttpPost]
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> PostRegisterVehicle(VehicleDTO vehicle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool result = await _vehicleService.RegisterVehicleAsync(vehicle);
                if (result)
                    return StatusCode(HttpStatusCode.Created);
                else
                    return StatusCode(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }
        }

        // Assumption to Record a Vehicle position
        /*
        * {
	        "UserId": "1",
            "DeviceId": "1",
	        "Latitude": 12.32,
            "Longitude": 24.42,
            "UpdateLocationTimeStamp": "2020-04-19 02:13:44"
          }
         */
        // PUT: api/Vehicles/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVehiclePosition(VehiclePositionDTO vehiclePosition)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool result = await _vehicleService.RecordVehiclePositionAsync(vehiclePosition);
                if (result)
                    return StatusCode(HttpStatusCode.OK);
                else
                    return StatusCode(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }
        }

        // Assumption to get Vehicle current position
        // GET: /api/Vehicles?userID=1&deviceId=1
        [HttpGet]
        [ResponseType(typeof(Vehicle))]
        [BasicAuthenticationAttribute]
        public async Task<IHttpActionResult> GetVehicleCurrentPosition(int userID, int deviceId)
        {

            VehiclePositionDTO vehicle = null;
            try
            {
                if (userID != 0 && deviceId != 0)
                {
                    vehicle = await _vehicleService.GetCurrentVehiclePositionAsync(userID, deviceId);
                    if (vehicle == null)
                    {
                        return NotFound();
                    }
                    // Bonus :) Calling Google Map Api for matching localities                     
                    vehicle.Locality.LocalityName = await _vehicleService.GetMatchingLocality(vehicle.Latitude + "," + vehicle.Longitude);
                }
                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }

            return Ok(vehicle);
        }

        // Assumption to get Vehicle current position
        // GET: api/Vehicles/1/2/sdate/edate
        [HttpGet]
        [ResponseType(typeof(Vehicle))]
        [BasicAuthenticationAttribute]
        public async Task<IHttpActionResult> GetVehiclePositionRange(int userID, int deviceID, DateTime startDate, DateTime endDate)
        {
            List<VehiclePositionDTO> vehicle = null;
            try
            {
                if (userID != 0 && deviceID != 0 && startDate != null && endDate != null)
                {
                    vehicle = await _vehicleService.GetVehiclePositionRangeAsync(userID, deviceID, startDate, endDate);

                    if (vehicle == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }

            return Ok(vehicle);
        }
    }
}