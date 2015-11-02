using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class EventScheduleRepository : IEventScheduleRepository
    {
        public CmsContext db = new CmsContext();

        public List<EventSchedule> GetEventSchedulesForEventTaskListId(int taskListId)
        {
            return db.EventSchedules.Where(e => e.EventTaskListId == taskListId).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
