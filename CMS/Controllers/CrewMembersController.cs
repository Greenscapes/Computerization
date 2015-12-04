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
    [RoutePrefix("api/crewmembers")]
    public class CrewMembersController : ApiController
    {
        private readonly CrewRepository db = new CrewRepository();

        // GET: api/CrewMembers
        [Route("")]
        public IEnumerable<CrewMemberViewModel> GetCrewMembers()
        {
            return db.GetCrewMembers().MapTo<IEnumerable<CrewMemberViewModel>>();
        }

        // GET: api/CrewMembers
        [Route("~/api/crews/{crewId:int}/members")]
        public IEnumerable<CrewMemberViewModel> GetCrewMembers(int crewId)
        {
            return db.GetCrewMembers().Where(m => m.CrewId == crewId).MapTo<IEnumerable<CrewMemberViewModel>>();
        }

        // GET: api/CrewMembers/5
        [Route("{id:int}")]
        [Route("~/api/crews/{crewId:int}/members/{id:int}")]
        [ResponseType(typeof(CrewMemberViewModel))]
        public IHttpActionResult GetCrewMember(int id)
        {
            var crewMember = db.GetCrewMember(id).MapTo<CrewMemberViewModel>();
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
        public IHttpActionResult PutCrewMember(int id, CrewMemberViewModel crewMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != crewMember.Id)
            {
                return BadRequest();
            }

            db.UpdateCrewMember(crewMember.MapTo<CrewMember>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CrewMembers
        [Route("")]
        [Route("~/api/crews/{crewId:int}/members")]
        [ResponseType(typeof(CrewMemberViewModel))]
        public IHttpActionResult PostCrewMember(CrewMemberViewModel crewMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateCrewMember(crewMember.MapTo<CrewMember>());

            return Ok(crewMember);
        }

        // DELETE: api/CrewMembers/5
        [Route("{id:int}")]
        [Route("~/api/crews/{crewId:int}/members/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteCrewMember(int id)
        {
            var success = db.DeleteCrewMember(id);
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