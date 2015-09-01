using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/types/crews")]
    public class CrewTypesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/CrewTypes
        [Route("")]
        public IQueryable<CrewType> GetCrewTypes()
        {
            return db.CrewTypes;
        }

        [Route("{id:int}/employees")]
        public IQueryable<Employee> GetEmployeesForCrewType(int id)
        {
            return db.CrewTypes.Where(c => c.Id == id).SelectMany(c => c.Employees);
        }

        // GET: api/CrewTypes/5
        [Route("{id:int}")]
        [ResponseType(typeof(CrewType))]
        public IHttpActionResult GetCrewType(int id)
        {
            var crewType = db.CrewTypes.Find(id);
            if (crewType == null)
            {
                return NotFound();
            }

            return Ok(crewType);
        }

        // PUT: api/CrewTypes/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCrewType(int id, CrewType crewType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != crewType.Id)
            {
                return BadRequest();
            }

            db.Entry(crewType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrewTypeExists(id))
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

        // POST: api/CrewTypes
        [Route("")]
        [ResponseType(typeof(CrewType))]
        public IHttpActionResult PostCrewType(CrewType crewType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CrewTypes.Add(crewType);
            db.SaveChanges();

            return Ok(crewType);
        }

        // DELETE: api/CrewTypes/5
        [Route("{id:int}")]
        [ResponseType(typeof(CrewType))]
        public IHttpActionResult DeleteCrewType(int id)
        {
            var crewType = db.CrewTypes.Find(id);
            if (crewType == null)
            {
                return NotFound();
            }

            db.CrewTypes.Remove(crewType);
            db.SaveChanges();

            return Ok(crewType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CrewTypeExists(int id)
        {
            return db.CrewTypes.Count(e => e.Id == id) > 0;
        }
    }
}