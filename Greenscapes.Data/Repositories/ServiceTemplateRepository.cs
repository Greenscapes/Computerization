using System;
using System.Collections.Generic;
using System.Linq;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class ServiceTemplateRepository : IServiceTemplateRepository
    {
        public CmsContext db = new CmsContext();

        public ServiceTemplate GetServiceTemplate(int id)
        {
            ServiceTemplate serviceTemplate = db.ServiceTemplates.FirstOrDefault(s => s.Id == id);
            if (serviceTemplate == null)
            {
                return null;
            }
            return serviceTemplate;
        }

        public List<ServiceTemplate> GetServiceTemplates()
        {
            return db.ServiceTemplates.OrderBy(s => s.Name).ToList();
        }

        public bool UpdateServiceTemplate(ServiceTemplate serviceTemplate)
        {
            var existingTemplate = db.ServiceTemplates.FirstOrDefault(p => p.Id == serviceTemplate.Id);
            if (existingTemplate != null)
            {
                existingTemplate.Name = serviceTemplate.Name;
                existingTemplate.UseTasks = serviceTemplate.UseTasks;
                existingTemplate.Url = serviceTemplate.Url;
                existingTemplate.JsonFields = serviceTemplate.JsonFields;
            }
            else
            {
                db.ServiceTemplates.Add(serviceTemplate);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteServiceTemplate(int id)
        {
            var serviceTemplate = db.ServiceTemplates.FirstOrDefault(p => p.Id == id);
            if (serviceTemplate == null)
                return false;

            db.ServiceTemplates.Remove(serviceTemplate);
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
