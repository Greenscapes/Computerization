namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EventTaskTime
    {
        public int Id { get; set; }

        public DateTime EventDate { get; set; }

        public int EventTaskListId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public virtual EventTaskList EventTaskList { get; set; }
    }
}
