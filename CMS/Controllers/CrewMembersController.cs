using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Greenscapes.Data.DataContext;

namespace CMS.Controllers
{
    [RoutePrefix("api/crewmembers")]
    public class CrewMembersController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/CrewMembers
        [Route("")]
        public IQueryable<CrewMember> GetCrewMembers()
        {
            return db.CrewMembers;
        }

        // GET: api/CrewMembers
        [Route("~/api/crews/{crewId:int}/members")]
        public IQueryable<CrewMember> GetCrewMembers(int crewId)
        {
            return db.CrewMembers.Where(m => m.CrewId == crewId);
        }

        // GET: api/CrewMembers/5
        [Route("{id:int}")]
        [Route("~/api/crews/{crewId:int}/members/{id:int}")]
        [ResponseType(typeof(CrewMember))]
        public IHttpActionResult GetCrewMember(int id)
        {
            var crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return NotFound();
            }

            return Ok(crewMember);
        }

        // PUT: api/CrewMembers/5
        [Route("{id:int}")]
        [Route("~/api/crews/{crewId:int}/members/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCrewMember(int id, CrewMember crewMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != crewMember.Id)
            {
                return BadRequest();
            }

            db.Entry(crewMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrewMemberExists(id))
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

        // POST: api/CrewMembers
        [Route("")]
        [Route("~/api/crews/{crewId:int}/members")]
        [ResponseType(typeof(CrewMember))]
        public IHttpActionResult PostCrewMember(CrewMember crewMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CrewMembers.Add(crewMember);
            db.SaveChanges();

            return Ok(crewMember);
        }

        // DELETE: api/CrewMembers/5
        [Route("{id:int}")]
        [Route("~/api/crews/{crewId:int}/members/{id:int}")]
        [ResponseType(typeof(CrewMember))]
        public IHttpActionResult DeleteCrewMember(int id)
        {
            var crewMember = db.CrewMembers.Find(id);
            if (crewMember == null)
            {
                return NotFound();
            }

            db.CrewMembers.Remove(crewMember);
            db.SaveChanges();

            return Ok(crewMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CrewMemberExists(int id)
        {
            return db.CrewMembers.Count(e => e.Id == id) > 0;
        }
    }
}