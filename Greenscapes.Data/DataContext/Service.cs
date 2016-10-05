namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            PropertyServices = new HashSet<PropertyService>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

       public virtual HashSet<PropertyService> PropertyServices { get; set; }
    }
}
