using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Greenscapes.Data.Models
{
    public class PropertyTaskList : ModelBase
    {
        public PropertyTaskList()
        {
            PropertyTasks = new List<PropertyTask>();
        }

        public int PropertyId
        {
            get;
            set;
        }

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
        public virtual Property Property
        {
            get;
            set;
        }

        public virtual PropertyTaskListType PropertyTaskListType
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

        protected bool Equals(PropertyTaskList other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyTaskList) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}