using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IPropertyRepository : IDisposable
    {
        Property GetProperty(int id);
        List<Property> GetProperties();
        bool UpdateProperty(Property property);
        bool DeleteProperty(int id);
    }
}
