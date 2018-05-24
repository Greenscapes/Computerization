namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyTaskHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropertyTaskHeader()
        {
            PropertyTaskDetails = new HashSet<PropertyTaskDetail>();
        }

        public int Id { get; set; }

        public int PropertyTaskListTypeId { get; set; }

        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyTaskDetail> PropertyTaskDetails { get; set; }

        public virtual PropertyTaskListType PropertyTaskListType { get; set; }
    }
}
