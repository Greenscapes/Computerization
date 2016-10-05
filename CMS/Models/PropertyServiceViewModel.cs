using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class PropertyServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventCount { get; set; }
        public int ServiceId { get; set; }
        public int PropertyId { get; set; }
    }
}