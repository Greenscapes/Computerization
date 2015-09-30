using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/eventschedules")]
    public class EventSchedulesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/EventSchedules
        [Route("")]
        public IEnumerable<EventScheduleDetails> GetEventSchedules()
        {
           
            var result = db.EventSchedules.AsEnumerable()
          .Select(e => new EventScheduleDetails()
          {
              start = e.StartTime.ToString("s"),
              end = e.EndTime.ToString("s"),
              text = e.Title,
              id = e.Id.ToString()
          });
           

              return result;
        }

        // GET: api/eventschedules
        [Route("{employeeId:int}/events")]
        public IEnumerable<EventSchedule> GetEventSchedules(int employeeId)
        {
            try
            {
                List<EventSchedule> events = new List<EventSchedule>();

                db.CrewMembers.Where(cm => cm.EmployeeId == employeeId).ToList().ForEach(m =>
                {
                    m.Crew.PropertyTasks.ToList().ForEach(p => events.AddRange(p.EventSchedules));
                });
                return events;
            }
            catch (Exception)
            {
                
                throw;
            }
             
        }

        // [Route("{id:int}/eventschedules'")]int taskId
        [Route("~/api/properties/:propertyId/tasklists/{taskListId:int}/tasks/{taskId:int}/eventschedules")]
        public IQueryable<EventScheduleDetails> GetEventSchedules(int propertyId, int taskListId,int taskId)
        {
            var result = db.EventSchedules.Where(e => e.PropertyTask.Id == taskId)
           .Select(e => new EventScheduleDetails()
           {
               start = e.StartTime.ToString("s"),
               end = e.EndTime.ToString("s"),
               text = e.Title,
               id = e.Id.ToString()
           });


            return result;
        }
        //[Route("")]
        //public IQueryable<EventSchedule> GetEventSchedules()
        //{
        //    return db.EventSchedules;
        //}
        // GET: api/EventSchedules/5
        [ResponseType(typeof(EventSchedule))]
        public IHttpActionResult GetEventSchedule(int id)
        {
            EventSchedule eventSchedule = db.EventSchedules.Find(id);
            if (eventSchedule == null)
            {
                return NotFound();
            }

            return Ok(eventSchedule);
        }

        // PUT: api/EventSchedules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventSchedule(int id, EventSchedule eventSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventSchedule.Id)
            {
                return BadRequest();
            }

            db.Entry(eventSchedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventScheduleExists(id))
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
        [ResponseType(typeof(EventSchedule))]
        public IHttpActionResult PostEventSchedule(EventSchedule eventSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventSchedules.Add(eventSchedule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventSchedule.Id }, eventSchedule);
        }

        // DELETE: api/EventSchedules/5
        [ResponseType(typeof(EventSchedule))]
        public IHttpActionResult DeleteEventSchedule(int id)
        {
            EventSchedule eventSchedule = db.EventSchedules.Find(id);
            if (eventSchedule == null)
            {
                return NotFound();
            }

            db.EventSchedules.Remove(eventSchedule);
            db.SaveChanges();

            return Ok(eventSchedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventScheduleExists(int id)
        {
            return db.EventSchedules.Count(e => e.Id == id) > 0;
        }
    }

    public class EventScheduleDetails
    {
        public string id { get; set; }
        public string text { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}