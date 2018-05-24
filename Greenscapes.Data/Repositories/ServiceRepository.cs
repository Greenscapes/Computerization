using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public CmsContext db = new CmsContext();

        public Service GetService(int id)
        {
            Service service = db.Services.FirstOrDefault(p => p.Id == id);
            if (service == null)
            {
                return null;
            }

            return service;
        }

        public List<Service> GetServices()
        {
            return db.Services.ToList();
        }

        public bool UpdateService(Service service)
        {
            var existingService = db.Services.FirstOrDefault(p => p.Id == service.Id);
            if (existingService != null)
            {
                existingService.Name = service.Name;
            }
            else
            {
                db.Services.Add(service);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteService(int id)
        {
            var service = db.Services.FirstOrDefault(p => p.Id == id);
            if (service == null || service.PropertyServices.Any())
                return false;

            db.Services.Remove(service);
            db.SaveChanges();
            return true;
        }

        public List<PropertyService> GetPropertyServices(int propertyId)
        {
            var propertyServices = db.PropertyServices.Where(p => p.PropertyId == propertyId).ToList();

            return propertyServices;
        }

        public PropertyService GetPropertyService(int id)
        {
            PropertyService service = db.PropertyServices.FirstOrDefault(p => p.Id == id);
            if (service == null)
            {
                return null;
            }

            return service;
        }

        public bool UpdatePropertyService(PropertyService service)
        {
            var existingService = db.PropertyServices.FirstOrDefault(p => p.Id == service.Id);
            if (existingService != null)
            {
                existingService.EventCount = service.EventCount;
                existingService.ServiceId = service.ServiceId;
            }
            else
            {
                db.PropertyServices.Add(service);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeletePropertyService(int id)
        {
            var service = db.PropertyServices.FirstOrDefault(p => p.Id == id);
  
            db.PropertyServices.Remove(service);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}