using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using System;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/eventnotes")]
    public class EventNotesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/Crews
        [Route("")]
        public IQueryable<PropertyTaskEventNote> GetPropertyTaskEventNotes()
        {
            return db.PropertyTaskEventNotes;
        }

        // GET: api/eventschedules
        [Route("{eventscheduleId:int}")]
        public IQueryable<PropertyTaskEventNote> GetPropertyTaskEventNotes(int eventscheduleId)
        {
            try
            {

                return db.PropertyTaskEventNotes.Where(pn => pn.EventScheduleId == eventscheduleId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [ResponseType(typeof(EventSchedule))]
        public IHttpActionResult GetPropertyTaskEventNote(int id)
        {
            PropertyTaskEventNote propertyTaskEventNote = db.PropertyTaskEventNotes.Find(id);
            if (propertyTaskEventNote == null)
            {
                return NotFound();
            }

            return Ok(propertyTaskEventNote);
        }
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyTaskEventNote(int id, PropertyTaskEventNote propertyTaskEventNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTaskEventNote.Id)
            {
                return BadRequest();
            }

            db.Entry(propertyTaskEventNote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTaskEventNoteExists(id))
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

        [Route("")]
        [ResponseType(typeof(PropertyTaskEventNote))]
        public IHttpActionResult PostPropertyTaskEventNote(PropertyTaskEventNote propertyTaskEventNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            propertyTaskEventNote.ReviewStatus = (int)ReviewStatusEnum.New;
            db.PropertyTaskEventNotes.Add(propertyTaskEventNote);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propertyTaskEventNote.Id }, propertyTaskEventNote);
        }

        // DELETE: api/PropertyTaskEventNote/5
        [ResponseType(typeof(PropertyTaskEventNote))]
        public IHttpActionResult DeletePropertyTaskEventNote(int id)
        {
            PropertyTaskEventNote propertyTaskEventNote = db.PropertyTaskEventNotes.Find(id);
            if (propertyTaskEventNote == null)
            {
                return NotFound();
            }

            db.PropertyTaskEventNotes.Remove(propertyTaskEventNote);
            db.SaveChanges();

            return Ok(propertyTaskEventNote);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool PropertyTaskEventNoteExists(int id)
        {
            return db.PropertyTaskEventNotes.Count(e => e.Id == id) > 0;
        }
    }
}