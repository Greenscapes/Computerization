using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IServiceRepository : IDisposable
    {
        Service GetService(int id);
        List<Service> GetServices();
        bool UpdateService(Service service);
        bool DeleteService(int id);
        List<PropertyService> GetPropertyServices(int propertyId);
        PropertyService GetPropertyService(int id);
        bool UpdatePropertyService(PropertyService service);
        bool DeletePropertyService(int id);
    }
}
