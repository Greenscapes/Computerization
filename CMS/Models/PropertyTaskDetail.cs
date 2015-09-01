using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class PropertyTaskDetail : ModelBase
    {
        public int PropertyTaskId
        {
            get;
            set;
        }

        public int PropertyTaskHeaderId
        {
            get;
            set;
        }

        public string Value
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

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual PropertyTaskHeader PropertyTaskHeader
        {
            get;
            set;
        }
    }
}