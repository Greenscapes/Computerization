namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            EventTaskLists = new HashSet<EventTaskList>();
            PropertyTaskLists = new HashSet<PropertyTaskList>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PropertyRefNumber { get; set; }

        public int PropertyType { get; set; }

        public int NumberOfFreeServiceCalls { get; set; }

        public DateTime ContractDate { get; set; }

        [StringLength(11)]
        public string Zip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTaskList> EventTaskLists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyTaskList> PropertyTaskLists { get; set; }
    }
}
