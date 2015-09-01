using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace CMS.Models
{
    public class WeeklySchedule : ModelBase
    {
        public WeeklySchedule()
        {
            SundaySchedule = new DailySchedule();
            MondaySchedule = new DailySchedule();
            TuesdaySchedule = new DailySchedule();
            WednesdaySchedule = new DailySchedule();
            ThursdaySchedule = new DailySchedule();
            FridaySchedule = new DailySchedule();
            SaturdaySchedule = new DailySchedule();
        }

        public int SundayScheduleId
        {
            get;
            set;
        }

        public int MondayScheduleId
        {
            get;
            set;
        }

        public int TuesdayScheduleId
        {
            get;
            set;
        }

        public int WednesdayScheduleId
        {
            get;
            set;
        }

        public int ThursdayScheduleId
        {
            get;
            set;
        }

        public int FridayScheduleId
        {
            get;
            set;
        }

        public int SaturdayScheduleId
        {
            get;
            set;
        }

        public virtual DailySchedule SundaySchedule
        {
            get;
            set;
        }

        public virtual DailySchedule MondaySchedule
        {
            get;
            set;
        }

        public virtual DailySchedule TuesdaySchedule
        {
            get;
            set;
        }

        public virtual DailySchedule WednesdaySchedule
        {
            get;
            set;
        }

        public virtual DailySchedule ThursdaySchedule
        {
            get;
            set;
        }

        public virtual DailySchedule FridaySchedule
        {
            get;
            set;
        }

        public virtual DailySchedule SaturdaySchedule
        {
            get;
            set;
        }
    }
}