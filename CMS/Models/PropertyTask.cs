using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class PropertyTask : ModelBase
    {
        public PropertyTask()
        {
            PropertyTaskDetails = new List<PropertyTaskDetail>();
            Crews = new List<Crew>();
        }

        public int PropertyTaskListId
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Notes
        {
            get;
            set;
        }

        public int EstimatedDuration
        {
            get;
            set;
        }
        public bool IsFreeService
        {
            get;
            set;
        }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual PropertyTaskList PropertyTaskList
        {
            get;
            set;
        }

        public virtual ICollection<PropertyTaskDetail> PropertyTaskDetails
        {
            get;
            set;
        }

        
        public virtual ICollection<Crew> Crews
        {
            get;
            set;
        }
        public virtual ICollection<EventSchedule> EventSchedules
        {
            get;
            set;
        }
    }
}