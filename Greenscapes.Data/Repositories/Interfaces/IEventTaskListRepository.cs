using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IEventTaskListRepository : IDisposable
    {
        EventTaskList GetEventTaskList(int id);
        List<EventTaskList> GetEventTaskLists(int propertyid, int taskId);
        EventTaskList UpdateEventTaskList(EventTaskList taskList);
        bool DeleteEventTaskList(int id);
    }
}
