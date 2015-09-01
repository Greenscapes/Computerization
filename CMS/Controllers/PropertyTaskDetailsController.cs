using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/taskdetails")]
    public class PropertyTaskDetailsController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/PropertyTaskDetails
        [Route("")]
        public IQueryable<PropertyTaskDetail> GetPropertyTaskDetails()
        {
            return db.PropertyTaskDetails;
        }

        // GET: api/PropertyTaskDetails
        [Route("~/tasks/{taskId:int}/taskdetails")]
        public IQueryable<PropertyTaskDetail> GetPropertyTaskDetails(int taskId)
        {
            return db.PropertyTaskDetails;
        }

        // GET: api/PropertyTaskDetails/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTaskDetail))]
        public IHttpActionResult GetPropertyTaskDetail(int id)
        {
            PropertyTaskDetail propertyTaskDetail = db.PropertyTaskDetails.Find(id);
            if (propertyTaskDetail == null)
            {
                return NotFound();
            }

            return Ok(propertyTaskDetail);
        }

        // PUT: api/PropertyTaskDetails/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyTaskDetail(int id, PropertyTaskDetail propertyTaskDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTaskDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(propertyTaskDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTaskDetailExists(id))
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

        // POST: api/PropertyTaskDetails
        [Route("")]
        [ResponseType(typeof(PropertyTaskDetail))]
        public IHttpActionResult PostPropertyTaskDetail(PropertyTaskDetail propertyTaskDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyTaskDetails.Add(propertyTaskDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propertyTaskDetail.Id }, propertyTaskDetail);
        }

        // DELETE: api/PropertyTaskDetails/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTaskDetail))]
        public IHttpActionResult DeletePropertyTaskDetail(int id)
        {
            PropertyTaskDetail propertyTaskDetail = db.PropertyTaskDetails.Find(id);
            if (propertyTaskDetail == null)
            {
                return NotFound();
            }

            db.PropertyTaskDetails.Remove(propertyTaskDetail);
            db.SaveChanges();

            return Ok(propertyTaskDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyTaskDetailExists(int id)
        {
            return db.PropertyTaskDetails.Count(e => e.Id == id) > 0;
        }
    }
}