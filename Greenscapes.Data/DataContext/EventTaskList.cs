namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EventTaskList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventTaskList()
        {
            EventSchedules = new HashSet<EventSchedule>();
            EventTaskTimes = new HashSet<EventTaskTime>();
            PropertyTasks = new HashSet<PropertyTask>();
            ServiceTickets = new HashSet<ServiceTicket>();
        }

        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int CrewId { get; set; }
        public int? PropertyServiceId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? ServiceTemplateId { get; set; }

        public decimal? NumberServices { get; set; }

        public virtual Crew Crew { get; set; }
        public virtual PropertyService PropertyService { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventSchedule> EventSchedules { get; set; }

        public virtual Property Property { get; set; }

        public virtual ServiceTemplate ServiceTemplate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTaskTime> EventTaskTimes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyTask> PropertyTasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceTicket> ServiceTickets { get; set; }
    }
}
