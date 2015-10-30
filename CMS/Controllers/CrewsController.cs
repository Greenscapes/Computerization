using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Greenscapes.Data.DataContext;

namespace CMS.Controllers
{
    [RoutePrefix("api/crews")]
    public class CrewsController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/Crews
        [Route("")]
        public IQueryable<Crew> GetCrews()
        {
            return db.Crews;
        }

        // GET: api/Crews/5
        [Route("{id:int}")]
        [ResponseType(typeof(Crew))]
        public IHttpActionResult GetCrew(int id)
        {
            var crew = db.Crews.Find(id);
            if (crew == null)
            {
                return NotFound();
            }

            return Ok(crew);
        }

        [Route("~/api/crews/employee/{id:int}")]
        [ResponseType(typeof(Crew))]
        public IHttpActionResult GetCrewByEmployeeId(int id)
        {
            var crewMember = db.CrewMembers.FirstOrDefault(c => c.EmployeeId == id);
            if (crewMember == null)
            {
                return NotFound();
            }

            return Ok(crewMember.Crew);
        }

        // PUT: api/Crews/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCrew(int id, Crew crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != crew.Id)
            {
                return BadRequest();
            }

            db.Entry(crew).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrewExists(id))
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

        // POST: api/Crews
        [Route("")]
        [ResponseType(typeof(Crew))]
        public IHttpActionResult PostCrew(Crew crew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Crews.Add(crew);
            db.SaveChanges();


            return Ok(crew);
        }

        // DELETE: api/Crews/5
        [Route("{id:int}")]
        [ResponseType(typeof(Crew))]
        public IHttpActionResult DeleteCrew(int id)
        {
            var crew = db.Crews.Find(id);
            if (crew == null)
            {
                return NotFound();
            }

            crew.CrewMembers.ToList().ForEach(m => db.CrewMembers.Remove(m));

            db.Crews.Remove(crew);
            db.SaveChanges();

            return Ok(crew);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CrewExists(int id)
        {
            return db.Crews.Count(e => e.Id == id) > 0;
        }
    }
}