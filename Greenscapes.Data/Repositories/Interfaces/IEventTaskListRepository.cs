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
        List<EventTaskList> GetEventTaskLists();
        EventTaskList UpdateEventTaskList(EventTaskList taskList);
        bool DeleteEventTaskList(int id);
        List<EventTaskList> GetEventTaskListsForCrew(int crewId);
        List<EventTaskList> GetPropertyEventTaskLists(int propertyId);
        void SetStartTime(int id, DateTime start);
        void SetFinishTime(int id, DateTime start);
    }
}
