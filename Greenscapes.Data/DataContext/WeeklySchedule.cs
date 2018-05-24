namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WeeklySchedule
    {
        public int Id { get; set; }

        public int SundayScheduleId { get; set; }

        public int MondayScheduleId { get; set; }

        public int TuesdayScheduleId { get; set; }

        public int WednesdayScheduleId { get; set; }

        public int ThursdayScheduleId { get; set; }

        public int FridayScheduleId { get; set; }

        public int SaturdayScheduleId { get; set; }

        public virtual DailySchedule DailySchedule { get; set; }

        public virtual DailySchedule DailySchedule1 { get; set; }

        public virtual DailySchedule DailySchedule2 { get; set; }

        public virtual DailySchedule DailySchedule3 { get; set; }

        public virtual DailySchedule DailySchedule4 { get; set; }

        public virtual DailySchedule DailySchedule5 { get; set; }

        public virtual DailySchedule DailySchedule6 { get; set; }
    }
}
