﻿using System;
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
using Greenscapes.Business.Services;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Models;
using Ical.Net.DataTypes;
using Ical.Net.Evaluation;

namespace CMS.Controllers
{
    [RoutePrefix("api/eventschedules")]
    public class EventSchedulesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/EventSchedules
        [Route("")]
        public IQueryable<EventSchedule> GetEventSchedules()
        {
            return db.EventSchedules;
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
                    m.Crew.EventTaskLists.ToList().ForEach(p => events.AddRange(p.EventSchedules));
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

                db.EventTaskLists.Where(pt => pt.PropertyId == propertyId).ToList()
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
                List<EventDetails> eventDetails = new List<EventDetails>();
           
                var service = new EventService();
                var eventSchedules = service.GetEventsForCrewForDay(from, crewid);

               foreach (var eventVal in eventSchedules)
               {
                    EventTaskList propTask = db.EventTaskLists.Include("Crew").Include("Property").Include("EventTaskTimes").Single(pt => pt.Id == eventVal.EventTaskListId);
                    if (propTask.Crew == null)
                    {
                        continue;
                    }
                    Crew crewVal = propTask.Crew;
                    if (crewVal == null)
                    {
                        continue;
                    }

                   if (!string.IsNullOrEmpty(eventVal.RecurrenceRule))
                   {
                       var pattern = new RecurrencePattern(eventVal.RecurrenceRule);
                       var evaluator = new RecurrencePatternEvaluator(pattern);
                       var items = evaluator.Evaluate(new CalDateTime(eventVal.StartTime), eventVal.StartTime,
                           eventVal.StartTime.AddYears(5), false);
                       if (!items.Any(i => i.StartTime.Date == selectedDate))
                           continue;
                   }
                   else
                   {
                       if (eventVal.StartTime.AddDays(-1) > selectedDate || eventVal.EndTime < selectedDate)
                           continue;
                   }

                    EventDetails eventDetail = new EventDetails();

                    TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    
                    eventDetail.EventId = eventVal.Id;
                   eventDetail.StartTime = TimeZoneInfo.ConvertTimeFromUtc(eventVal.StartTime, estZone);
                    eventDetail.EndTime = TimeZoneInfo.ConvertTimeFromUtc(eventVal.EndTime, estZone);
                    eventDetail.ActualEndTime = eventVal.ActualEndTime;
                   eventDetail.ActualStartTime = eventVal.ActualStartTime;
                   eventDetail.IsAllDay = eventVal.IsAllDay;
                   eventDetail.RecurrenceRule = eventVal.RecurrenceRule;
                   eventDetail.RecurrenceID = eventVal.RecurrenceID;
                   eventDetail.RecurrenceException = eventVal.RecurrenceException;
                   eventDetail.StartTimezone = eventVal.StartTimezone;
                   eventDetail.EndTimezone = eventVal.EndTimezone;
                   eventDetail.Status = (StatusEnum)eventVal.Status;
                   eventDetail.Title = eventVal.Title;
                   eventVal.EventTaskList =propTask;
                   eventDetail.TaskId = propTask.Id;
                   eventDetail.EventTaskListId = eventVal.EventTaskListId;
                   eventDetail.PropertyId = propTask.Property.Id;
                   eventDetail.PropertyAddress = propTask.Property.Address1 + " "+
                                                 propTask.Property.Address2 + " "+
                                                     propTask.Property.City.Name + " "+
                                                         propTask.Property.State + " "+
                                                             propTask.Property.Zip ;

                   eventDetail.PropertyName = eventVal.EventTaskList.Property.Name;
                   eventDetail.PropertyRefNumber = eventVal.EventTaskList.Property.PropertyRefNumber;
                   var eventTaskTime = propTask.EventTaskTimes.FirstOrDefault(e => e.EventDate.Subtract(selectedDate) == TimeSpan.Zero);
                   if (eventTaskTime != null)
                   {
                       eventDetail.TaskStartTime = eventTaskTime.StartTime;
                       eventDetail.TaskEndTime = eventTaskTime.EndTime;
                   }
                   eventDetails.Add(eventDetail);
               }  
                  

                return eventDetails.OrderBy(e => e.StartTime);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Route("{crewid:int}/crewevents")]
        public IEnumerable<EventDetails> GetEventSchedulesForCrew(int crewid)
        {
            try
            {
                //int crewid = 3;
                DateTime selectedDate = DateTime.Now;
                DateTime from = DateTime.Now.AddYears(-1);
                DateTime to = DateTime.Now.AddYears(1);
                List<EventDetails> eventDetails = new List<EventDetails>();

                var eventSchedules = db.EventSchedules.Where(ev => (ev.StartTime <= from) && (ev.EndTime >= from) ||
                                                                   (ev.StartTime <= to) && (ev.EndTime >= to) ||
                                                                    (ev.StartTime >= from) && (ev.EndTime <= to)

                    ).OrderBy(e => e.StartTime).ToList();

                foreach (var eventVal in eventSchedules)
                {
                    var eventTaskLists = db.EventTaskLists.Single(pt => pt.Id == eventVal.EventTaskListId);
                    if (eventTaskLists.Crew == null || eventTaskLists.CrewId != crewid)
                    {
                        continue;
                    }
                    Crew crewVal = eventTaskLists.Crew;
                    if (crewVal == null)
                    {
                        continue;
                    }
                    EventDetails eventDetail = new EventDetails();

                    eventDetail.EventId = eventVal.Id;
                    eventDetail.StartTime = eventVal.StartTime;
                    eventDetail.EndTime = eventVal.EndTime;
                    eventDetail.ActualEndTime = eventVal.ActualEndTime;
                    eventDetail.ActualStartTime = eventVal.ActualStartTime;
                    eventDetail.IsAllDay = eventVal.IsAllDay;
                    eventDetail.RecurrenceRule = eventVal.RecurrenceRule;
                    eventDetail.RecurrenceID = eventVal.RecurrenceID;
                    eventDetail.RecurrenceException = eventVal.RecurrenceException;
                    eventDetail.StartTimezone = eventVal.StartTimezone;
                    eventDetail.EndTimezone = eventVal.EndTimezone;
                    eventDetail.Status = (StatusEnum)eventVal.Status;
                    eventDetail.Title = eventVal.Title;
                    eventVal.EventTaskList = eventTaskLists;
                    eventDetail.TaskId = eventTaskLists.Id;
                    eventDetail.PropertyId = eventTaskLists.Property.Id;
                    eventDetail.PropertyAddress = eventTaskLists.Property.Address1 + " " +
                                                  eventTaskLists.Property.Address2 + " " +
                                                      eventTaskLists.Property.City.Name + " " +
                                                          eventTaskLists.Property.State + " " +
                                                              eventTaskLists.Property.Zip;

                    eventDetail.PropertyName = eventVal.EventTaskList.Property.Name;
                    eventDetail.PropertyRefNumber = eventVal.EventTaskList.Property.PropertyRefNumber;
                    eventDetail.Crew = crewVal;
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
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public int PropertyTaskId { get; set; }
       
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyRefNumber { get; set; }
        public string PropertyAddress { get; set; }
        public Crew Crew { get; set; }
        public int EventTaskListId { get; set; }
        public DateTime? TaskStartTime { get; set; }
        public DateTime? TaskEndTime { get; set; }
    }
     
}