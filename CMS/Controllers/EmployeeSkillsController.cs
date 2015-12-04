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
    [RoutePrefix("api/types/crewlists")]
    public class EmployeeSkillsController : ApiController
    {
        private readonly CrewRepository db = new CrewRepository();

        [Route("")]
        public IEnumerable<EmployeeSkillsViewModel> GetEmployeeSkills()
        {
            return db.GetEmployeeSkills().MapTo<IEnumerable<EmployeeSkillsViewModel>>();
        }

        [Route("{id:int}/employees")]
        public IEnumerable<EmployeeViewModel> GetEmployeesForSkill(int id)
        {
            var employeeSkill = db.GetEmployeeSkill(id);

            return employeeSkill.Employees.MapTo<IEnumerable<EmployeeViewModel>>();
        }

        [Route("{id:int}")]
        [ResponseType(typeof(EmployeeSkillsViewModel))]
        public IHttpActionResult GetEmployeeSkill(int id)
        {
            var employeeSkill = db.GetEmployeeSkill(id);
            if (employeeSkill == null)
            {
                return NotFound();
            }

            return Ok(employeeSkill.MapTo<EmployeeSkillsViewModel>());
        }

        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeSkill(int id, EmployeeSkillsViewModel employeeSkills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeSkills.Id)
            {
                return BadRequest();
            }

            db.UpdateEmployeeSkill(employeeSkills.MapTo<EmployeeSkill>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("")]
        [ResponseType(typeof(EmployeeSkillsViewModel))]
        public IHttpActionResult PostEmployeeSkill(EmployeeSkillsViewModel employeeSkills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateEmployeeSkill(employeeSkills.MapTo<EmployeeSkill>());

            return Ok(employeeSkills);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteEmployeeSkills(int id)
        {
            var success = db.DeleteEmployeeSkill(id);
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