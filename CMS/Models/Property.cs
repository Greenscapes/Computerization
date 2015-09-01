using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class Property : ModelBase
    {
        public Property()
        {
            PropertyTaskLists = new HashSet<PropertyTaskList>();
        }

        public string Name
        {
            get;
            set;
        }

        public string Address1
        {
            get;
            set;
        }

        public string Address2
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string Zip
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<PropertyTaskList> PropertyTaskLists
        {
            get;
            set;
        }
    }
}