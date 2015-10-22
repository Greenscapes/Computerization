using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/Employees
        [Route("")]
        public IQueryable<Employee> GetEmployees()
        {
            var employees = db.Employees;
            foreach (var employee in employees)
            {
                employee.InCrew = db.CrewMembers.Any(c => c.EmployeeId == employee.Id);
            }

            return employees;
        }

        // GET: api/Employees/5
        [Route("{id:int}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            var originalEmployee = db.Employees.Find(id);
            var originalEntry = db.Entry(originalEmployee);
            originalEntry.CurrentValues.SetValues(employee);

            foreach (var missing in originalEmployee.CrewTypes.Where(c => employee.CrewTypes.All(ec => ec.Id != c.Id)).ToList())
            {
                originalEmployee.CrewTypes.Remove(missing);
            }

            foreach (var newlyAdded in employee.CrewTypes.Where(c => originalEmployee.CrewTypes.All(ec => ec.Id != c.Id)).ToList())
            {
                db.Set<CrewType>().Attach(newlyAdded);
                originalEmployee.CrewTypes.Add(newlyAdded);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [Route("")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Set<Employee>().Attach(employee);
            employee.CrewTypes.ToList().ForEach(c => db.Set<CrewType>().Attach(c));

            db.Employees.Add(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        // DELETE: api/Employees/5
        [Route("{id:int}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}