using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class PropertyTaskViewModel
    {
        public int Id { get; set; }

        public int PropertyTaskListId { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public int EstimatedDuration { get; set; }

        public bool IsFreeService { get; set; }

        public int Status { get; set; }

        public int EventTaskListId { get; set; }

        public string ScheduleName { get; set; }

        //     public List<CrewViewModel> Crews { get; set; } 
        public CrewViewModel Crew { get; set; }
    }
}
