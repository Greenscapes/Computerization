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

        public void SetFreeService(int propertyId, int eventTaskListId, bool isFreeService, DateTime serviceDate)
        {
            var freeService =
                db.FreeServices.FirstOrDefault(f => f.EventTaskListId == eventTaskListId && f.ServiceTime == serviceDate);
            if (freeService != null && !isFreeService)
            {
                db.FreeServices.Remove(freeService);
                var property = db.Properties.FirstOrDefault(p => p.Id == propertyId);
                if (property != null)
                {
                    property.NumberOfFreeServiceCalls++;
                }
            }
            else if (freeService == null && isFreeService)
            {
                var newFreeService = new FreeService();
                newFreeService.ServiceTime = serviceDate;
                newFreeService.EventTaskListId = eventTaskListId;
                db.FreeServices.Add(newFreeService);

                var property = db.Properties.FirstOrDefault(p => p.Id == propertyId);
                if (property != null)
                {
                    property.NumberOfFreeServiceCalls--;
                }
            }
            db.SaveChanges();
        }
    }
}
