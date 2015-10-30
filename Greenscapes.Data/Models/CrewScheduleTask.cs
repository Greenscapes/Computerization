using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Greenscapes.Data.Models
{
    public class CrewScheduleTask : ModelBase
    {
        public int CrewScheduleId
        {
            get;
            set;
        }

        public int PropertyTaskId
        {
            get;
            set;
        }

        public string AdditionalNotes
        {
            get;
            set;
        }

        public int ActualMinutes
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CrewSchedule CrewSchedule
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual PropertyTask PropertyTask
        {
            get;
            set;
        }
    }
}