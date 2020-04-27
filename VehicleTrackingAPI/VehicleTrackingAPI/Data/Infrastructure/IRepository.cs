using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingAPI.Models;

namespace VehicleTrackingAPI.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
    }
}
