using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Business.Services.Interfaces;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Business.Services
{
    public class EventService : IEventService
    {
        public IEventTaskListRepository EventTaskListRepository = new EventTaskListRepository();
        public IEventScheduleRepository EventScheduleRepository = new EventScheduleRepository();

        public List<EventSchedule> GetEventsForCrewForDay(DateTime day, int crewId)
        {
            var events = new List<EventSchedule>();

            var taskLists = EventTaskListRepository.GetEventTaskListsForCrew(crewId);
            foreach (var taskList in taskLists)
            {
                events.AddRange(EventScheduleRepository.GetEventSchedulesForEventTaskListId(taskList.Id));
            }

            return events;
        }
    }
}
