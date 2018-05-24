using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IEventScheduleRepository : IDisposable
    {
        List<EventSchedule> GetEventSchedulesForEventTaskListId(int taskListId);
    }
}
