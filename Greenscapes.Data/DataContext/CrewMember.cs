namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CrewMember
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int CrewId { get; set; }

        public bool IsCrewLeader { get; set; }

        public virtual Crew Crew { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
