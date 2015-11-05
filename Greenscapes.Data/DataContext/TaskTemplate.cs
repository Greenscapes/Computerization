namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskTemplate()
        {

        }

        public int Id { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public int EstimatedDuration { get; set; }

        public bool IsFreeService { get; set; }
    }
}
