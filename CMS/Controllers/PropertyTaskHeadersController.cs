using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/headers")]
    public class PropertyTaskHeadersController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/PropertyTaskHeaders
        [Route("")]
        public IQueryable<PropertyTaskHeader> GetPropertyTaskHeaders()
        {
            return db.PropertyTaskHeaders;
        }

        [Route("~/api/properties/{propertyId:int}/tasklists/{taskListId:int}/headers")]
        public IQueryable<PropertyTaskHeader> GetPropertyTaskHeaders(int taskListId)
        {
            var taskList = db.PropertyTaskLists.Find(taskListId);
            if (taskList == null)
            {
                return null;
            }

            var taskListType = db.PropertyTaskListTypes.Find(taskList.PropertyTaskListTypeId);
            if (taskListType == null)
            {
                return null;
            }

            return db.PropertyTaskHeaders.Where(h => h.PropertyTaskListTypeId == taskListType.Id);
        }

        // GET: api/PropertyTaskHeaders/5
        [Route("{id:int}")]
        [Route("~/api/properties/{propertyId:int}/tasklists/{taskListId:int}/headers/{id:int}")]
        [ResponseType(typeof(PropertyTaskHeader))]
        public IHttpActionResult GetPropertyTaskHeader(int id)
        {
            PropertyTaskHeader propertyTaskHeader = db.PropertyTaskHeaders.Find(id);
            if (propertyTaskHeader == null)
            {
                return NotFound();
            }

            return Ok(propertyTaskHeader);
        }

        // PUT: api/PropertyTaskHeaders/5
        [Route("{id:int}")]
        [Route("~/api/properties/{propertyId:int}/tasklists/{taskListId:int}/headers/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyTaskHeader(int id, PropertyTaskHeader propertyTaskHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTaskHeader.Id)
            {
                return BadRequest();
            }

            db.Entry(propertyTaskHeader).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTaskHeaderExists(id))
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

        // POST: api/PropertyTaskHeaders
        [Route("")]
        [Route("~/api/properties/{propertyId:int}/tasklists/{taskListId:int}/headers")]
        [ResponseType(typeof(PropertyTaskHeader))]
        public IHttpActionResult PostPropertyTaskHeader(PropertyTaskHeader propertyTaskHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyTaskHeaders.Add(propertyTaskHeader);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propertyTaskHeader.Id }, propertyTaskHeader);
        }

        // DELETE: api/PropertyTaskHeaders/5
        [Route("{id:int}")]
        [Route("~/api/properties/{propertyId:int}/tasklists/{taskListId:int}/headers/{id:int}")]
        [ResponseType(typeof(PropertyTaskHeader))]
        public IHttpActionResult DeletePropertyTaskHeader(int id)
        {
            PropertyTaskHeader propertyTaskHeader = db.PropertyTaskHeaders.Find(id);
            if (propertyTaskHeader == null)
            {
                return NotFound();
            }

            db.PropertyTaskHeaders.Remove(propertyTaskHeader);
            db.SaveChanges();

            return Ok(propertyTaskHeader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyTaskHeaderExists(int id)
        {
            return db.PropertyTaskHeaders.Count(e => e.Id == id) > 0;
        }
    }
}