using System;

namespace Greenscapes.Data.DataContext
{
    public class ServiceTicket 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceTicket()
        {
            //Implement this if you need to have GS Staff explicitly tied to employees
            //Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public int ServiceTemplateId { get; set; }

        public int EventTaskListId { get; set; }

        public DateTime EventDate { get; set; }

        public string ReferenceNumber { get; set; }

        public DateTime? VisitFromTime { get; set; }

        public DateTime? VisitToTime { get; set; }

        public string Staff { get; set; }

        public string JsonFields { get; set; }

        public int? ApprovedById { get; set; }

        public DateTime? ApprovedDate { get; set; }
        
        public string Notes { get; set; }

        public int? Condition { get; set; }

        //public virtual ServiceTemplate ServiceTemplate { get; set; }

        //public virtual PropertyTask PropertyTask { get; set; }

        //public virtual Employee ApprovedBy { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Employee> Employees { get; set; }
    }
}