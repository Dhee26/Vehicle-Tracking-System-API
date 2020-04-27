using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingAPI.Data.Infrastructure;
using VehicleTrackingAPI.Data.Repository.Vehicle;
using VehicleTrackingAPI.DTO;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        IRepository<Vehicle> VehicleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<VehicleDevice> VehicleDeviceRepository { get; }
        IRepository<Location> LocationRepository { get; }

    }
}
