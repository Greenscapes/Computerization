using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class EventTaskListScheduleViewModel
    {
        public string Name { get; set; }
        public int EventCount { get; set; }
        public List<EventScheduleViewModel> Schedules { get; set; }
        public int Id { get; set; }
    }

    public class EventScheduleViewModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public decimal EventNumber { get; set; }
        public bool FreeService { get; set; }
        public DateTime DateTime { get; set; }
    }
}