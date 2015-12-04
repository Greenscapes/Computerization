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

        EmployeeSkill GetEmployeeSkill(int id);
        List<EmployeeSkill> GetEmployeeSkills();
        bool UpdateEmployeeSkill(EmployeeSkill employeeSkill);
        bool DeleteEmployeeSkill(int id);

        CrewMember GetCrewMember(int id);
        List<CrewMember> GetCrewMembers();
        bool UpdateCrewMember(CrewMember crewMember);
        bool DeleteCrewMember(int id);
    }
}