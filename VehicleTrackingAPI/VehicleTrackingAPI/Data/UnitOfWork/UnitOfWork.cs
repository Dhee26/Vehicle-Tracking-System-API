using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VehicleTrackingAPI.Data.Infrastructure;
using VehicleTrackingAPI.Data.Repository.Vehicle;
using VehicleTrackingAPI.DTO;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private VehicleTrackingAPIContext _dbContext;
        public UnitOfWork(VehicleTrackingAPIContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        void IDisposable.Dispose()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }

        #region Implement Repository

        private IRepository<Vehicle> _VehicleRepository;
        public IRepository<Vehicle> VehicleRepository
        {
            get { return _VehicleRepository ?? (_VehicleRepository = new RepositoryBase<Vehicle>(_dbContext)); }
        }

        // Implement Repository User
        private IRepository<User> _UserRepository;
        public IRepository<User> UserRepository
        {
            get { return _UserRepository ?? (_UserRepository = new RepositoryBase<User>(_dbContext)); }
        }

        public IRepository<VehicleDevice> _VehicleDeviceRepository;
        public IRepository<VehicleDevice> VehicleDeviceRepository
        {
            get { return _VehicleDeviceRepository ?? (_VehicleDeviceRepository = new RepositoryBase<VehicleDevice>(_dbContext)); }
        }

        public IRepository<Location> _LocationRepository;
        public IRepository<Location> LocationRepository
        {
            get { return _LocationRepository ?? (_LocationRepository = new RepositoryBase<Location>(_dbContext)); }
        }

        #endregion
    }
}