using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Greenscapes.Data.Models
{
    public class EventSchedule:ModelBase
    {
       [IgnoreDataMember]
        public Guid ClientTaskId { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartTime { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public int PropertyTaskId
        {
            get;
            set;
        }
       [JsonIgnore]
        [IgnoreDataMember]
        public PropertyTask PropertyTask { get; set; }
       [JsonIgnore]
       public ICollection<PropertyTaskEventNote> PropertyTaskEventNotes { get; set; }
    }
     
}