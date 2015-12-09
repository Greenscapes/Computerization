namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskTemplate
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public int EstimatedDuration { get; set; }

        public bool IsFreeService { get; set; }

        public int DisplayOrder { get; set; }
    }
}
