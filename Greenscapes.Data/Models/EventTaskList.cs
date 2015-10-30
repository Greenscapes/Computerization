using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Greenscapes.Data.Models
{
    public class EventTaskList : ModelBase
    {
        public EventTaskList()
        {
            PropertyTasks = new List<PropertyTask>();
        }

        public int PropertyId
        {
            get;
            set;
        }

        public int CrewId { get; set; }

        public string Name
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Property Property
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Crew Crew
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<PropertyTask> PropertyTasks
        {
            get;
            set;
        }
    }
}