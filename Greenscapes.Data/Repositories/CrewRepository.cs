using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class CrewRepository : ICrewRepository
    {
        public CmsContext db = new CmsContext();

        public Crew GetCrew(int id)
        {
            Crew Crew = db.Crews.FirstOrDefault(p => p.Id == id);
            if (Crew == null)
            {
                return null;
            }

            return Crew;
        }

        public List<Crew> GetCrews()
        {
            return db.Crews.ToList();
        }

        public bool UpdateCrew(Crew Crew)
        {
            var existingCrew = db.Crews.FirstOrDefault(p => p.Id == Crew.Id);
            if (existingCrew != null)
            {
                existingCrew.Name = Crew.Name;
                
                db.SaveChanges();
            }
            else
            {
                db.Crews.Add(Crew);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteCrew(int id)
        {
            var Crew = db.Crews.FirstOrDefault(p => p.Id == id);
            if (Crew == null)
                return false;

            db.Crews.Remove(Crew);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
