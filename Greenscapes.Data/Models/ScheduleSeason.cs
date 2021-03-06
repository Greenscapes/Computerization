﻿using System.Collections.Generic;

namespace Greenscapes.Data.Models
{
    public class ScheduleSeason : ModelBase
    {
        public ScheduleSeason()
        {
            WeeklySchedules = new List<WeeklySchedule>();
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public virtual ICollection<WeeklySchedule> WeeklySchedules
        {
            get;
            set;
        }
    }
}