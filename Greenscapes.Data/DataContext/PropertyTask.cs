namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropertyTask()
        {
            PropertyTaskDetails = new HashSet<PropertyTaskDetail>();
            Crews = new HashSet<Crew>();
        }

        public int Id { get; set; }

        public int PropertyTaskListId { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public int EstimatedDuration { get; set; }

        public bool IsFreeService { get; set; }

        public int Status { get; set; }

        public int? EventTaskListId { get; set; }

        public virtual EventTaskList EventTaskList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyTaskDetail> PropertyTaskDetails { get; set; }

        public virtual PropertyTaskList PropertyTaskList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Crew> Crews { get; set; }
    }
}
