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
            PropertyTasks = new HashSet<PropertyTask>();
        }

        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int CrewId { get; set; }

        public int? ServiceTemplateId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public virtual Crew Crew { get; set; }

        public virtual Property Property { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyTask> PropertyTasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventSchedule> EventSchedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ServiceTemplate ServiceTemplate { get; set; }
    }
}
