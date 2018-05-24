using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Greenscapes.Data.Models;

namespace CMS.Models
{
    public class PropertyViewModel
    {
        public int Id
        {
            get; set;
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

        public int CityId
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

        public CustomerTypeEnum CustomerType { get; set; }

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

        public int TaskListId { get; set; }

        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string CityName { get; set; }
    }
}