using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Kendo.Mvc.UI;

namespace CMS.Models
{
    public class EventViewModel : ISchedulerEvent
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime start;

        public DateTime Start
        {
            get { return start; }
            set { start = value.ToUniversalTime(); }
        }

        public string StartTimezone { get; set; }

        private DateTime end;

        public DateTime End
        {
            get { return end; }
            set { end = value.ToUniversalTime(); }
        }

        public string EndTimezone { get; set; }

        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public int? OwnerID { get; set; }

        public EventSchedule ToEntity()
        {
            return new EventSchedule()
            {
                Id = TaskID,
                Title = Title,
                StartTime = Start,
                StartTimezone = StartTimezone,
                EndTime = End,
                EndTimezone = EndTimezone,
                Description = Description,
                RecurrenceRule = RecurrenceRule,
                RecurrenceException = RecurrenceException,
                RecurrenceID = RecurrenceID,
                IsAllDay = IsAllDay,
                EventTaskListId = OwnerID ?? 0
            };
        }

    }
}