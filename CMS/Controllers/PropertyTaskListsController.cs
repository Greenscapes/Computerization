using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/tasklists")]
    public class PropertyTaskListsController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/PropertyTaskLists
        [Route("")]
        public IQueryable<PropertyTaskList> GetPropertyTaskLists()
        {
            return db.PropertyTaskLists;
        }

        // GET: api/PropertyTaskLists
        [Route("~/api/properties/{propertyId:int}/tasklists")]
        public IQueryable<PropertyTaskList> GetPropertyTaskLists(int propertyId)
        {
            return db.PropertyTaskLists.Where(t => t.PropertyId == propertyId);
        }

        // GET: api/PropertyTaskLists/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTaskList))]
        public IHttpActionResult GetPropertyTaskList(int id)
        {
            PropertyTaskList propertyTaskList = db.PropertyTaskLists.Find(id);
            if (propertyTaskList == null)
            {
                return NotFound();
            }

            return Ok(propertyTaskList);
        }

        // PUT: api/PropertyTaskLists/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyTaskList(int id, PropertyTaskList propertyTaskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTaskList.Id)
            {
                return BadRequest();
            }

            db.Entry(propertyTaskList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTaskListExists(id))
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

        // POST: api/PropertyTaskLists
        [Route("")]
        [ResponseType(typeof(PropertyTaskList))]
        public IHttpActionResult PostPropertyTaskList(PropertyTaskList propertyTaskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyTaskLists.Add(propertyTaskList);
            db.SaveChanges();

            return Ok(propertyTaskList);
            //return CreatedAtRoute("DefaultApi", new { id = propertyTaskList.Id }, propertyTaskList);
        }

        // DELETE: api/PropertyTaskLists/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTaskList))]
        public IHttpActionResult DeletePropertyTaskList(int id)
        {
            var propertyTaskList = db.PropertyTaskLists.Find(id);
            if (propertyTaskList == null)
            {
                return NotFound();
            }

            var tasks = propertyTaskList.PropertyTasks.ToList();
            tasks.ForEach(t =>
            {
                t.PropertyTaskDetails.ToList().ForEach(d => db.PropertyTaskDetails.Remove(d));
                foreach (var evenSchedule in t.EventSchedules)
                {
                     db.EventSchedules.Remove(evenSchedule);
                }
                db.PropertyTasks.Remove(t);
            });
            db.PropertyTaskLists.Remove(propertyTaskList);
            db.SaveChanges();

            return Ok(propertyTaskList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyTaskListExists(int id)
        {
            return db.PropertyTaskLists.Count(e => e.Id == id) > 0;
        }
    }
}