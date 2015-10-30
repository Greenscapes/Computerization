namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceTemplate()
        {
            ServiceTickets = new HashSet<ServiceTicket>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public string JsonFields { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceTicket> ServiceTickets { get; set; }
    }
}
