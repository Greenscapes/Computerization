using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Mappers;
using CMS.Models;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;

namespace CMS.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        private readonly EmployeeRepository db = new EmployeeRepository();
        private readonly CmsContext context = new CmsContext();

        // GET: api/Employees
        [Route("")]
        public IEnumerable<EmployeeViewModel> GetEmployees()
        {
            var employees = db.GetEmployees().MapTo<List<EmployeeViewModel>>();
            foreach (var employee in employees)
            {
                employee.InCrew = context.CrewMembers.Any(c => c.EmployeeId == employee.Id);
            }

            return employees;
        }

        // GET: api/Employees/5
        [Route("{id:int}")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            var employee = db.GetEmployee(id).MapTo<EmployeeViewModel>();
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.UpdateEmployee(employee.MapTo<Employee>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id:int}/crews")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeCrews(int id, List<int> crewIds)
        {
            db.UpdateEmployeeCrews(id, crewIds);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [Route("")]
        [ResponseType(typeof(EmployeeViewModel))]
        public IHttpActionResult PostEmployee(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var id = db.UpdateEmployee(employee.MapTo<Employee>());
            employee.Id = id;
            return Ok(employee);
        }

        // DELETE: api/Employees/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var success = db.DeleteEmployee(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}