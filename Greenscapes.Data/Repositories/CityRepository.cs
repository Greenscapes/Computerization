using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        public CmsContext db = new CmsContext();

        public City GetCity(int id)
        {
            City city = db.Cities.FirstOrDefault(p => p.Id == id);
            if (city == null)
            {
                return null;
            }

            return city;
        }

        public List<City> GetCities()
        {
            return db.Cities.ToList();
        }

        public bool UpdateCity(City city)
        {
            var existingCity = db.Cities.FirstOrDefault(p => p.Id == city.Id);
            if (existingCity != null)
            {
                existingCity.Name = city.Name;
            }
            else
            {
                db.Cities.Add(city);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteCity(int id)
        {
            var city = db.Cities.FirstOrDefault(p => p.Id == id);
            if (city == null || city.Properties.Any())
                return false;

            db.Cities.Remove(city);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}