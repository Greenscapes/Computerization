using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class TaskTemplateViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public int EstimatedDuration { get; set; }

        public bool IsFreeService { get; set; }
    }
}
