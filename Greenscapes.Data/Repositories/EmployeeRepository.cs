using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public CmsContext db = new CmsContext();

        public Employee GetEmployee(int id)
        {
            Employee employee = db.Employees.Include("EmployeeSkills").FirstOrDefault(p => p.Id == id);
            if (employee == null)
            {
                return null;
            }

            return employee;
        }

        public List<Employee> GetEmployees()
        {
            return db.Employees.Include("EmployeeSkills").Include("CrewMembers").ToList();
        }

        public int UpdateEmployee(Employee employee)
        {
            var existingEmployee = db.Employees.Include("EmployeeSkills").FirstOrDefault(p => p.Id == employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Email = employee.Email;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.MiddleName = employee.MiddleName;
                existingEmployee.Phone = employee.Phone;

                foreach (var missing in existingEmployee.EmployeeSkills.Where(c => employee.EmployeeSkills.All(ec => ec.Id != c.Id)).ToList())
                {
                    existingEmployee.EmployeeSkills.Remove(missing);
                }

                foreach (var newlyAdded in employee.EmployeeSkills.Where(c => existingEmployee.EmployeeSkills.All(ec => ec.Id != c.Id)).ToList())
                {
                    db.Set<EmployeeSkill>().Attach(newlyAdded);
                    existingEmployee.EmployeeSkills.Add(newlyAdded);
                }

                db.SaveChanges();
            }
            else
            {
                db.Set<Employee>().Attach(employee);
                employee.EmployeeSkills.ToList().ForEach(c => db.Set<EmployeeSkill>().Attach(c));

                db.Employees.Add(employee);
            }

            db.SaveChanges();

            return employee.Id;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = db.Employees.FirstOrDefault(p => p.Id == id);
            if (employee == null)
                return false;

            foreach (var crewMember in db.CrewMembers.Where(c => c.EmployeeId == id))
            {
                db.CrewMembers.Remove(crewMember);
            }

            db.Employees.Remove(employee);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void UpdateEmployeeCrews(int id, List<int> crewIds)
        {
            var idsToRemove = new List<int>();
            var employee = db.Employees.FirstOrDefault(p => p.Id == id);
            if (employee == null)
                return;

            foreach (var crewmember in employee.CrewMembers)
            {
                idsToRemove.Add(crewmember.Id);
            }

            foreach (var idToRemove in idsToRemove)
            {
                var crewmember = db.CrewMembers.First(i => i.Id == idToRemove);
                db.CrewMembers.Remove(crewmember);
                db.SaveChanges();
            }

            foreach (var crewId in crewIds)
            {
                var crewMember = new CrewMember();
                crewMember.EmployeeId = id;
                crewMember.CrewId = crewId;
                employee.CrewMembers.Add(crewMember);
            }

            db.SaveChanges();
        }
    }
}
