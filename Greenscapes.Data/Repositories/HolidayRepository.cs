using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        public CmsContext db = new CmsContext();

        public Holiday GetHoliday(int id)
        {
            Holiday holiday = db.Holidays.FirstOrDefault(p => p.Id == id);
            if (holiday == null)
            {
                return null;
            }

            return holiday;
        }

        public List<Holiday> GetHolidays()
        {
            return db.Holidays.ToList();
        }

        public bool UpdateHoliday(Holiday holiday)
        {
            var existingHoliday = db.Holidays.FirstOrDefault(p => p.Id == holiday.Id);
            if (existingHoliday != null)
            {
                existingHoliday.Name = holiday.Name;
            }
            else
            {
                db.Holidays.Add(holiday);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteHoliday(int id)
        {
            var holiday = db.Holidays.FirstOrDefault(p => p.Id == id);

            db.Holidays.Remove(holiday);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}