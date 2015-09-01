using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class CrewMember : ModelBase
    {
        public int EmployeeId
        {
            get;
            set;
        }

        public int CrewId
        {
            get;
            set;
        }

        public bool IsCrewLeader
        {
            get;
            set;
        }

        public virtual Employee Employee
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
    }
}