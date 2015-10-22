using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CMS.Models
{
    public class Employee : ModelBase
    {
        public Employee()
        {
            CrewTypes = new List<CrewType>();
        }

        public string FirstName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email 
        { 
            get; set; 
        }
        public string Phone
        {
            get;
            set;
        }

        [NotMapped]
        public bool InCrew { get; set; }

        public virtual ICollection<CrewType> CrewTypes
        {
            get;
            set;
        }
    }
}