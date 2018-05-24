using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IHolidayRepository : IDisposable
    {
        Holiday GetHoliday(int id);
        List<Holiday> GetHolidays();
        bool UpdateHoliday(Holiday holiday);
        bool DeleteHoliday(int id);
    }
}
