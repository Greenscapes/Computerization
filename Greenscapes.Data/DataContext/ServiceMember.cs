namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceMember
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ServiceTicketId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ServiceTicket ServiceTicket { get; set; }
    }
}
