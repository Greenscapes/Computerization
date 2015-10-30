using System;
using System.Collections.Generic;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IServiceTemplateRepository : IDisposable
    {
        ServiceTemplate GetServiceTemplate(int id);
        List<ServiceTemplate> GetServiceTemplates();
        bool UpdateServiceTemplate(ServiceTemplate serviceTemplate);
        bool DeleteServiceTemplate(int id);
    }
}
