using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Data.Infrastructure
{
    public class RepositoryBase<T> : Disposable, IRepository<T> where T : class
    {
        private readonly DbContext _dataContext;

        private IDbSet<T> Dbset
        {
            get { return _dataContext.Set<T>(); }
        }                

        public RepositoryBase(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual T Add(T entity)
        {
            Dbset.Add(entity);
            return entity;
        }
    }
}