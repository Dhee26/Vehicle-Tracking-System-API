using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using VehicleTrackingAPI.Authentication;
using VehicleTrackingAPI.Data;
using VehicleTrackingAPI.Data.Repository.Vehicle;
using VehicleTrackingAPI.Data.UnitOfWork;
using VehicleTrackingAPI.Service.Vehicle;

namespace VehicleTrackingAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IVehicleService, VehicleService>();
            container.RegisterType<IVehicleRepository, VehicleRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();    
            container.RegisterType<DbContext, VehicleTrackingAPIContext>(new PerThreadLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}