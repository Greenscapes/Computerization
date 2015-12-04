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

        public bool UpdateCrew(Crew crew)
        {
            var existingCrew = db.Crews.FirstOrDefault(p => p.Id == crew.Id);
            if (existingCrew != null)
            {
                existingCrew.Name = crew.Name;
                
                db.SaveChanges();
            }
            else
            {
                db.Crews.Add(crew);
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

        public EmployeeSkill GetEmployeeSkill(int id)
        {
            EmployeeSkill employeeSkill = db.EmployeeSkills.FirstOrDefault(p => p.Id == id);
            if (employeeSkill == null)
            {
                return null;
            }

            return employeeSkill;
        }

        public List<EmployeeSkill> GetEmployeeSkills()
        {
            return db.EmployeeSkills.ToList();
        }

        public bool UpdateEmployeeSkill(EmployeeSkill employeeSkill)
        {
            var existingCrew = db.EmployeeSkills.FirstOrDefault(p => p.Id == employeeSkill.Id);
            if (existingCrew != null)
            {
                existingCrew.Description = employeeSkill.Description;
                existingCrew.Name = employeeSkill.Description;

                db.SaveChanges();
            }
            else
            {
                db.EmployeeSkills.Add(employeeSkill);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteEmployeeSkill(int id)
        {
            var employeeSkill = db.EmployeeSkills.FirstOrDefault(p => p.Id == id);
            if (employeeSkill == null)
                return false;

            db.EmployeeSkills.Remove(employeeSkill);
            db.SaveChanges();
            return true;
        }

        public CrewMember GetCrewMember(int id)
        {
            CrewMember crewMember = db.CrewMembers.FirstOrDefault(p => p.Id == id);
            if (crewMember == null)
            {
                return null;
            }

            return crewMember;
        }

        public List<CrewMember> GetCrewMembers()
        {
            return db.CrewMembers.ToList();
        }

        public bool UpdateCrewMember(CrewMember crewMember)
        {
            var existingCrew = db.CrewMembers.FirstOrDefault(p => p.Id == crewMember.Id);
            if (existingCrew != null)
            {
                existingCrew.CrewId = crewMember.CrewId;
                existingCrew.EmployeeId = crewMember.EmployeeId;
                existingCrew.IsCrewLeader = crewMember.IsCrewLeader;

                db.SaveChanges();
            }
            else
            {
                db.CrewMembers.Add(crewMember);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteCrewMember(int id)
        {
            var crewMember = db.CrewMembers.FirstOrDefault(p => p.Id == id);
            if (crewMember == null)
                return false;

            db.CrewMembers.Remove(crewMember);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
