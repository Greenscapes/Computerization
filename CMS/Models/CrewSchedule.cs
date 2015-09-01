using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class CrewSchedule : ModelBase
    {
        public CrewSchedule()
        {
            CrewScheduleTasks = new HashSet<CrewScheduleTask>();
        }

        public int CrewId
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
        public virtual ICollection<CrewScheduleTask> CrewScheduleTasks
        {
            get;
            set;
        } 
    }
}