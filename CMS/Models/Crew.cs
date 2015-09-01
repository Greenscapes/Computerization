using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class Crew : ModelBase
    {
        public Crew()
        {
            CrewMembers = new List<CrewMember>();
            WeeklySchedule = new WeeklySchedule();
        }

        public int CrewTypeId
        {
            get;
            set;
        }

        public int WeeklyScheduleId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public virtual CrewType CrewType
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual WeeklySchedule WeeklySchedule
        {
            get;
            set;
        }

        public virtual ICollection<CrewMember> CrewMembers
        {
            get;
            set;
        }
    }
}