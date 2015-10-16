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
using System.Globalization;

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
        // GET: api/eventschedules
        [Route("{propertyId:int}/{allProperty:bool}/propertyevents")]
        public IEnumerable<EventSchedule> GetEventSchedules(int propertyId, bool allProperty)
        {
            try
            {
                List<EventSchedule> events = new List<EventSchedule>();

                db.PropertyTasks.Where(pt => pt.PropertyTaskList.PropertyId == propertyId).ToList()
                    .ForEach(p => events.AddRange(p.EventSchedules));
                
                return events;
            }
            catch (Exception)
            {

                throw;
            }

        }
        // GET: api/eventschedules
        [Route("{year:int}/{month:int}/{date:int}/{crewid:int}/events")]
        public IEnumerable<EventDetails> GetEventSchedules(int year, int month, int date, int crewid)
        {
            try
            {
                //int crewid = 3;
                DateTime selectedDate = new DateTime(year, month, date);
                DateTime from = GetFirstDayOfWeek(selectedDate, null);
                DateTime to = GetLastDayOfWeek(selectedDate,null);
                List<EventDetails> eventDetails = new List<EventDetails>();

                var eventSchedules = db.EventSchedules.Where(ev => (ev.StartTime <= from) && (ev.EndTime >= from)||
                                                                   (ev.StartTime <= to) && (ev.EndTime >= to)||
                                                                    (ev.StartTime >= from) && (ev.EndTime <= to)
                                                                    
                    ).OrderBy(e=>e.StartTime).ToList();
           
               foreach (var eventVal in eventSchedules)
               {
                   PropertyTask propTask = db.PropertyTasks.Single(pt => pt.Id == eventVal.PropertyTaskId);
                   if(propTask.Crews== null || propTask.Crews.Count < 1)
                   {
                       continue;
                   }
                   Crew crewVal = propTask.Crews.FirstOrDefault(c=>c.Id == crewid );
                   if(crewVal == null)
                   {
                       continue;
                   }
                   EventDetails eventDetail = new EventDetails();

                   eventDetail.EventId = eventVal.Id;
                   eventDetail.StartTime = eventVal.StartTime;
                   eventDetail.EndTime = eventVal.EndTime;
                   eventDetail.Description = eventVal.Title;
                   eventVal.PropertyTask =propTask;
                   eventDetail.TaskId = propTask.Id;
                   eventDetail.PropertyId = propTask.PropertyTaskList.Property.Id;
                   eventDetail.PropertyAddress = propTask.PropertyTaskList.Property.Address1 + " "+
                                                 propTask.PropertyTaskList.Property.Address2 + " "+
                                                     propTask.PropertyTaskList.Property.City + " "+
                                                         propTask.PropertyTaskList.Property.State + " "+
                                                             propTask.PropertyTaskList.Property.Zip ;

                   eventDetail.PropertyName = eventVal.PropertyTask.PropertyTaskList.Property.Name;
                   eventDetail.PropertyRefNumber = eventVal.PropertyTask.PropertyTaskList.Property.PropertyRefNumber;
                   eventDetail.Crew=crewVal;
                   if (eventVal.PropertyTaskEventNotes != null)
                   {
                       eventDetail.EventNotes = eventVal.PropertyTaskEventNotes;
                   }
                   eventDetails.Add(eventDetail);
               }  
                  

                return eventDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /*
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
        }*/
        //[Route("")]
        //public IQueryable<EventSchedule> GetEventSchedules()
        //{
        //    return db.EventSchedules;
        //}
        // GET: api/EventSchedules/5
        [ResponseType(typeof(EventSchedule))]
        [Route("{id:int}")]
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
        [Route("{id:int}")]
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

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }

            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        public static DateTime GetLastDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }

            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek.AddDays(7);
        }
    }

    public class EventScheduleDetails
    {
        public string id { get; set; }
        public string text { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }

    public class EventDetails
    {
        public int EventId { get; set; }
        public int TaskId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyRefNumber { get; set; }
        public string PropertyAddress { get; set; }
        public ICollection<PropertyTaskEventNote> EventNotes { get; set; }
        public Crew Crew { get; set; }
       
    }
     
}