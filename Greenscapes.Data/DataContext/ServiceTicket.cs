using System;
using System.Collections.Generic;

namespace Greenscapes.Data.DataContext
{
    public class ServiceTicket 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceTicket()
        {
            ServiceMembers = new HashSet<ServiceMember>();
        }

        public int Id { get; set; }

        public int ServiceTemplateId { get; set; }

        public int EventTaskListId { get; set; }

        public DateTime EventDate { get; set; }

        public string ReferenceNumber { get; set; }

        public DateTime? VisitFromTime { get; set; }

        public DateTime? VisitToTime { get; set; }
        
        public string JsonFields { get; set; }

        public int? ApprovedById { get; set; }

        public DateTime? ApprovedDate { get; set; }
        
        public string Notes { get; set; }

        public int? Condition { get; set; }

        public virtual ServiceTemplate ServiceTemplate { get; set; }

        public virtual EventTaskList EventTaskList { get; set; }

        public virtual Employee ApprovedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceMember> ServiceMembers { get; set; }
    }
}