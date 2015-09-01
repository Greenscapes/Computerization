using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/tasks")]
    public class PropertyTasksController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/PropertyTasks
        [Route("")]
        public IQueryable<PropertyTask> GetPropertyTasks()
        {
            return db.PropertyTasks;
        }

        // GET: api/PropertyTasks
        [Route("~/api/tasklists/{taskListId:int}/tasks")]
        public IQueryable<PropertyTask> GetPropertyTasks(int taskListId)
        {
            return db.PropertyTasks.Where(p => p.PropertyTaskListId == taskListId);
        }

        // GET: api/PropertyTasks/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTask))]
        public IHttpActionResult GetPropertyTask(int id)
        {
            var propertyTask = db.PropertyTasks.Find(id);
            if (propertyTask == null)
            {
                return NotFound();
            }

            return Ok(propertyTask);
        }

        // PUT: api/PropertyTasks/5
        [Route("{id:int}")]
        [Route("~/api/tasklists/{taskListId:int}/tasks/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyTask(int id, PropertyTask propertyTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTask.Id)
            {
                return BadRequest();
            }

            db.Entry(propertyTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTaskExists(id))
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

        // POST: api/PropertyTasks
        [Route("")]
        [Route("~/api/tasks/:taskListId/tasks")]
        [ResponseType(typeof(PropertyTask))]
        public IHttpActionResult PostPropertyTask(PropertyTask propertyTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyTasks.Add(propertyTask);
            db.SaveChanges();

            return Ok(propertyTask);
        }

        // DELETE: api/PropertyTasks/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTask))]
        public IHttpActionResult DeletePropertyTask(int id)
        {
            var propertyTask = db.PropertyTasks.Find(id);
            if (propertyTask == null)
            {
                return NotFound();
            }

            propertyTask.PropertyTaskDetails.ToList().ForEach(d => db.PropertyTaskDetails.Remove(d));
            db.PropertyTasks.Remove(propertyTask);
            db.SaveChanges();

            return Ok(propertyTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyTaskExists(int id)
        {
            return db.PropertyTasks.Count(e => e.Id == id) > 0;
        }
    }
}