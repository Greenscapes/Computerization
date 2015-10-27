using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using System;

namespace CMS.Controllers
{
    [RoutePrefix("api/properties")]
    public class PropertiesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/Properties
        [Route("")]
        public IQueryable<Property> GetProperties()
        {
            return db.Properties;
        }

        // GET: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(Property))]
        public IHttpActionResult GetProperty(int id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            if (property.PropertyTaskLists.Any())
            {
                property.TaskListId = property.PropertyTaskLists.First().Id;
            }

            return Ok(property);
        }

        // PUT: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.Id)
            {
                return BadRequest();
            }

            db.Entry(property).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Properties
        [Route("")]
        [ResponseType(typeof(Property))]
        public IHttpActionResult PostProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (property.ContractDate == DateTime.MinValue)
            { property.ContractDate = DateTime.Now; }
          
            db.Properties.Add(property);
            db.SaveChanges();

            return Ok(property);
        }

        // DELETE: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(Property))]
        public IHttpActionResult DeleteProperty(int id)
        {
            var property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            var taskLists = property.PropertyTaskLists.ToList();
            taskLists.ForEach(l =>
            {
                var tasks = l.PropertyTasks.ToList();
                tasks.ForEach(t =>
                {
                    t.PropertyTaskDetails.ToList().ForEach(d => db.PropertyTaskDetails.Remove(d));

                    t.EventSchedules.ToList().ForEach(e =>
                    {
                        db.EventSchedules.Remove(e);
                    });
                    db.PropertyTasks.Remove(t);
                });
                db.PropertyTaskLists.Remove(l);
            });

            db.Properties.Remove(property);
            db.SaveChanges();

            return Ok(property);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.Id == id) > 0;
        }
    }
}