using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface ICityRepository : IDisposable
    {
        City GetCity(int id);
        List<City> GetCities();
        bool UpdateCity(City city);
        bool DeleteCity(int id);
    }
}