using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IPropertyTaskRepository : IDisposable
    {
        PropertyTask GetPropertyTask(int id);
        List<PropertyTask> GetPropertyTasksForTaskList(int propertyTaskListId);
        List<PropertyTask> GetPropertyTasksForProperty(int propertyId);
        bool UpdatePropertyTask(PropertyTask propertyTask);
        bool DeletePropertyTask(int id);
    }
}
