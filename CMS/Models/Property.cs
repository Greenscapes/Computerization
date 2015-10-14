using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System;

namespace CMS.Models
{
    public class Property : ModelBase
    {
        public Property()
        {
            PropertyTaskLists = new HashSet<PropertyTaskList>();
        }
      
        [Required]
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
        public string PropertyRefNumber { get; set; }

        public PropertyTypeEnum PropertyType { get; set; }

        public int NumberOfFreeServiceCalls { get; set; }

       [DataType(DataType.Date)]
        public DateTime ContractDate { get; set; }

        [StringLength(11)]
       // [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$")]
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