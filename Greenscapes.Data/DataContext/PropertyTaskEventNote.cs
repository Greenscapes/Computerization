namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyTaskEventNote
    {
        public int Id { get; set; }

        public string Notes { get; set; }

        public int ReviewStatus { get; set; }

        public int EventScheduleId { get; set; }

        public virtual EventSchedule EventSchedule { get; set; }
    }
}
