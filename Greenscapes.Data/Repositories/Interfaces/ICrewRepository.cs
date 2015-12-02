using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    internal interface ICrewRepository : IDisposable
    {
        Crew GetCrew(int id);
        List<Crew> GetCrews();
        bool UpdateCrew(Crew crew);
        bool DeleteCrew(int id);
    }
}