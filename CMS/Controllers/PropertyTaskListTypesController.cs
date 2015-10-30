using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using Greenscapes.Data.DataContext;

namespace CMS.Controllers
{
    [RoutePrefix("api/types/tasklists")]
    public class PropertyTaskListTypesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/PropertyTaskListTypes
        [Route("")]
        public IQueryable<PropertyTaskListType> GetPropertyTaskListTypes()
        {
            return db.PropertyTaskListTypes;
        }

        // GET: api/PropertyTaskListTypes/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTaskListType))]
        public IHttpActionResult GetPropertyTaskListType(int id)
        {
            PropertyTaskListType propertyTaskListType = db.PropertyTaskListTypes.Find(id);
            if (propertyTaskListType == null)
            {
                return NotFound();
            }

            return Ok(propertyTaskListType);
        }

        [Route("{id:int}/count")]
        public IHttpActionResult GetPropertyTaskListCount(int id)
        {
            return Ok(new {Count = db.PropertyTaskLists.Count(t => t.PropertyTaskListTypeId == id)});
        }

        // PUT: api/PropertyTaskListTypes/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyTaskListType(int id, PropertyTaskListType propertyTaskListType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTaskListType.Id)
            {
                return BadRequest();
            }

            var originalPropertyTaskListType = db.PropertyTaskListTypes
                .Where(t => t.Id == propertyTaskListType.Id)
                .Include(t => t.PropertyTaskHeaders)
                .SingleOrDefault();

            if (originalPropertyTaskListType == null)
            {
                return NotFound();
            }

            var originalEntry = db.Entry(originalPropertyTaskListType);
            originalEntry.CurrentValues.SetValues(propertyTaskListType);

            foreach (var header in propertyTaskListType.PropertyTaskHeaders)
            {
                var originalHeader = originalPropertyTaskListType.PropertyTaskHeaders
                    .SingleOrDefault(h => h.Id == header.Id && h.Id != 0);

                if(originalHeader != null)
                {
                    if (string.IsNullOrWhiteSpace(header.Name))
                    {
                        db.PropertyTaskDetails.Where(d => d.PropertyTaskHeaderId == originalHeader.Id)
                            .ToList()
                            .ForEach(d => db.PropertyTaskDetails.Remove(d));
                        db.PropertyTaskHeaders.Remove(originalHeader);
                    }
                    else
                    {
                        var headerEntry = db.Entry(originalHeader);
                        headerEntry.CurrentValues.SetValues(header);
                    }
                }
                else
                {
                    header.Id = 0;
                    originalPropertyTaskListType.PropertyTaskHeaders.Add(header);
                }
            }

            foreach (var originalHeader in originalPropertyTaskListType.PropertyTaskHeaders.Where(h => h.Id != 0).Where(originalHeader => propertyTaskListType.PropertyTaskHeaders.All(h => h.Id != originalHeader.Id)).ToList())
            {
                db.PropertyTaskDetails.Where(d => d.PropertyTaskHeaderId == originalHeader.Id)
                            .ToList()
                            .ForEach(d => db.PropertyTaskDetails.Remove(d));
                db.PropertyTaskHeaders.Remove(originalHeader);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTaskListTypeExists(id))
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

        // POST: api/PropertyTaskListTypes
        [Route("")]
        [ResponseType(typeof(PropertyTaskListType))]
        public IHttpActionResult PostPropertyTaskListType(PropertyTaskListType propertyTaskListType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyTaskListTypes.Add(propertyTaskListType);
            db.SaveChanges();

            return Ok(propertyTaskListType);
        }

        // DELETE: api/PropertyTaskListTypes/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTaskListType))]
        public IHttpActionResult DeletePropertyTaskListType(int id)
        {
            var propertyTaskListType = db.PropertyTaskListTypes.Find(id);
            if (propertyTaskListType == null)
            {
                return NotFound();
            }

            propertyTaskListType.PropertyTaskHeaders.ToList().ForEach(h =>
            {
                db.PropertyTaskDetails.Where(d => d.PropertyTaskHeaderId == h.Id)
                    .ToList()
                    .ForEach(d =>
                    {
                        db.PropertyTaskDetails.Remove(d);
                    });
                db.PropertyTaskHeaders.Remove(h);
                propertyTaskListType.PropertyTaskHeaders.Remove(h);
            });
            
            db.PropertyTaskListTypes.Remove(propertyTaskListType);
            db.SaveChanges();

            return Ok(propertyTaskListType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyTaskListTypeExists(int id)
        {
            return db.PropertyTaskListTypes.Count(e => e.Id == id) > 0;
        }
    }
}