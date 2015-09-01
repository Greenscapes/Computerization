using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class CrewType : TypeBase
    {
        public CrewType()
        {
            Employees = new List<Employee>();
        }

        [JsonIgnore]
        public virtual ICollection<Employee> Employees
        {
            get;
            set;
        }
    }
}