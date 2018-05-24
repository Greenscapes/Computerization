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
    [RoutePrefix("api/crews")]
    public class CrewsController : ApiController
    {
        private readonly CrewRepository repository = new CrewRepository();
    
        // GET: api/Crews
        [Route("")]
        public IEnumerable<CrewViewModel> GetCrews()
        {
            return repository.GetCrews().MapTo<IEnumerable<CrewViewModel>>();
        }

        // GET: api/Crews/5
        [Route("{id:int}")]
        [ResponseType(typeof(CrewViewModel))]
        public IHttpActionResult GetCrew(int id)
        {
            var crew = repository.GetCrew(id).MapTo<CrewViewModel>();
            if (crew == null)
            {
                return NotFound();
            }

            return Ok(crew);
        }

        [Route("~/api/crews/employee/{id:int}")]
        [ResponseType(typeof(CrewViewModel))]
        public IHttpActionResult GetCrewByEmployeeId(int id)
        {
            var crewMember = repository.GetCrewMembers().FirstOrDefault(c => c.EmployeeId == id);
            if (crewMember == null)
            {
                return NotFound();
            }

            return Ok(crewMember.Crew.MapTo<CrewViewModel>());
        }

        // PUT: api/Crews/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCrew(int id, CrewViewModel crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != crew.Id)
            {
                return BadRequest();
            }

            repository.UpdateCrew(crew.MapTo<Crew>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Crews
        [Route("")]
        [ResponseType(typeof(CrewViewModel))]
        public IHttpActionResult PostCrew(CrewViewModel crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.UpdateCrew(crew.MapTo<Crew>());

            return Ok(crew);
        }

        // DELETE: api/Crews/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteCrew(int id)
        {
            var success = repository.DeleteCrew(id);
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
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}