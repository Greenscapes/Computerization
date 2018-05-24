namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyTaskList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropertyTaskList()
        {
            PropertyTasks = new HashSet<PropertyTask>();
        }

        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int PropertyTaskListTypeId { get; set; }

        public string Name { get; set; }

        public virtual Property Property { get; set; }

        public virtual PropertyTaskListType PropertyTaskListType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyTask> PropertyTasks { get; set; }
    }
}
