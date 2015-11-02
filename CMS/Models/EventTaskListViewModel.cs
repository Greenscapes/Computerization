using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class EventTaskListViewModel
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int CrewId { get; set; }

        public int ServiceTemplateId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public List<PropertyTaskViewModel> PropertyTasks { get; set; } 
    }
}
