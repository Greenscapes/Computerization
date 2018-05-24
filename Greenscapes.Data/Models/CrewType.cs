using System.Collections.Generic;
using Newtonsoft.Json;

namespace Greenscapes.Data.Models
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