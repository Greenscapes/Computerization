using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class PropertyTaskHeader : ModelBase
    {
        public int PropertyTaskListTypeId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual PropertyTaskListType PropertyTaskListType
        {
            get;
            set;
        }
    }
}