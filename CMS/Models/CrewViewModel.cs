using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class CrewViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CrewMemberViewModel> CrewMembers { get; set; } 
    }

    public class CrewMemberViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int CrewId { get; set; }

        public bool IsCrewLeader { get; set; }
    }
}
