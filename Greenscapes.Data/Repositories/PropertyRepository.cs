using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        public CmsContext db = new CmsContext();

        public Property GetProperty(int id)
        {
            Property property = db.Properties.Include("PropertyTaskLists").FirstOrDefault(p => p.Id == id);
            if (property == null)
            {
                return null;
            }

            if (property.PropertyTaskLists.Any())
            {
                property.TaskListId = property.PropertyTaskLists.First().Id;
            }

            return property;
        }

        public List<Property> GetProperties()
        {
            return db.Properties.Include("City").ToList();
        }

        public bool UpdateProperty(Property property)
        {
            var existingProperty = db.Properties.FirstOrDefault(p => p.Id == property.Id);
            if (existingProperty != null)
            {
                existingProperty.Address1 = property.Address1;
                existingProperty.Address2 = property.Address2;
                existingProperty.City = property.City;
                existingProperty.ContractDate = property.ContractDate;
                existingProperty.Name = property.Name;
                existingProperty.NumberOfFreeServiceCalls = property.NumberOfFreeServiceCalls;
                existingProperty.PropertyRefNumber = property.PropertyRefNumber;
                existingProperty.State = property.State;
                existingProperty.PropertyType = property.PropertyType;
                existingProperty.CustomerType = property.CustomerType;
                existingProperty.Zip = property.Zip;
            }
            else
            {
                if (property.ContractDate == DateTime.MinValue)
                { property.ContractDate = DateTime.Now; }
                db.Properties.Add(property);

                var propertyTaskList = new PropertyTaskList();
                propertyTaskList.Property = property;
                propertyTaskList.Name = "Default";
                propertyTaskList.PropertyTaskListType = db.PropertyTaskListTypes.First();
                db.PropertyTaskLists.Add(propertyTaskList);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteProperty(int id)
        {
            var property = db.Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return false;

            db.Properties.Remove(property);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
