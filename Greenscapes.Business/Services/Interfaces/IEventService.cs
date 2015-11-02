using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Business.Services.Interfaces
{
    public interface IEventService
    {
        List<EventSchedule> GetEventsForCrewForDay(DateTime day, int crewId);
    }
}
