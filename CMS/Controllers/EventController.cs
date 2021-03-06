﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using CMS.Models;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Ical.Net.DataTypes;
using Ical.Net.Evaluation;
using Newtonsoft.Json;

namespace CMS.Controllers
{
    public class EventController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        public IEnumerable<EventViewModel> GetTask([ModelBinder(typeof(Models.ModelBinders.DataSourceRequestModelBinder))] DataSourceRequest request)
        {
            var data = db.EventSchedules.ToList().Select(task => new EventViewModel()
            {
                TaskID = task.Id,
                Title = task.Title,
                Description = task.Description,
                Start = DateTime.SpecifyKind(task.StartTime, DateTimeKind.Utc),
                End = DateTime.SpecifyKind(task.EndTime, DateTimeKind.Utc),
                IsAllDay = task.IsAllDay,
                RecurrenceID = task.RecurrenceID,
                RecurrenceRule = task.RecurrenceRule,
                RecurrenceException = task.RecurrenceException,
                StartTimezone = task.StartTimezone,
                EndTimezone = task.EndTimezone,
                StartDate = task.StartTime,
                OwnerID = task.EventTaskListId
            });

            return data;
        }

        // GET api/Task/5
        public EventSchedule GetTask(int id)
        {
            EventSchedule task = db.EventSchedules.Single(p => p.Id == id);
            if (task == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return task;
        }

        // PUT api/Task/5
        public HttpResponseMessage PutTask(EventViewModel task)
        {
            if (ModelState.IsValid)
            {
                var eventTaskList = db.EventTaskLists.Include("Property").Include("Crew").FirstOrDefault(e => e.Id == task.OwnerID);
                if (eventTaskList != null)
                {
                    task.Title = eventTaskList.Property.Name + ", " + eventTaskList.Crew.Name;
                }

                var entity = new EventSchedule
                {
                    Id = task.TaskID,
                    Title = task.Title,
                    StartTime = task.Start,
                    StartTimezone = task.StartTimezone,
                    EndTime = task.End,
                    EndTimezone = task.EndTimezone,
                    Description = task.Description,
                    RecurrenceRule = task.RecurrenceRule,
                    RecurrenceException = task.RecurrenceException,
                    RecurrenceID = task.RecurrenceID,
                    IsAllDay = task.IsAllDay,
                    EventTaskListId = task.OwnerID ?? 0
                };

                db.EventSchedules.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new[] { task });
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = task.TaskID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Task
        public HttpResponseMessage PostTask(EventViewModel task)
        {
            //var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            //var models = httpContext.Request.Form["models"];

            //var task = JsonConvert.DeserializeObject<EventViewModel>(models);
            if (ModelState.IsValid)
            {
                var eventTaskList = db.EventTaskLists.Include("Property").Include("Crew").FirstOrDefault(e => e.Id == task.OwnerID);
                if (eventTaskList != null)
                {
                    task.Title = eventTaskList.Property.Name + ", " + eventTaskList.Crew.Name;
                }

                List<DateTime> holidays;

                using (var holidayRepo = new HolidayRepository())
                {
                    holidays = holidayRepo.GetHolidays().Select(h => h.HolidayDate.Date).ToList();
                }

                if (holidays.Contains(task.StartDate.Date))
                {
                    var message = "Your schedule includes a holiday on: " + task.StartDate.Date;
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.Conflict, err);
                }

                if (!string.IsNullOrEmpty(task.RecurrenceRule))
                {
                    var pattern = new RecurrencePattern(task.RecurrenceRule);
                    var evaluator = new RecurrencePatternEvaluator(pattern);
                    var items = evaluator.Evaluate(new CalDateTime(task.StartDate), task.Start,
                        task.StartDate.AddDays(1), false);

                    var allItems = evaluator.Evaluate(new CalDateTime(task.StartDate), task.Start,
                        task.End.AddYears(5), true); //Adding 5 years since the end date is equal to the start date. The recurrence rule handles our occurences

                    var holiday = allItems.FirstOrDefault(i => holidays.Contains(i.StartTime.Date));
                    if (holiday != null)
                    {
                        var message = "Your schedule includes a holiday on: " + holiday.StartTime.Date;
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.Conflict, err);
                    }
                        
                    if (!items.Any())
                    {
                        var startEntity = new EventSchedule
                        {
                            Id = task.TaskID,
                            Title = task.Title,
                            StartTime = task.Start,
                            StartTimezone = task.StartTimezone,
                            EndTime = task.End,
                            EndTimezone = task.EndTimezone,
                            Description = task.Description,
                            RecurrenceRule = string.Empty,
                            RecurrenceException = string.Empty,
                            RecurrenceID = null,
                            IsAllDay = task.IsAllDay,
                            EventTaskListId = task.OwnerID ?? 0
                        };

                        db.EventSchedules.Add(startEntity);
                    }
                }

                var entity = new EventSchedule
                {
                    Id = task.TaskID,
                    Title = task.Title,
                    StartTime = task.Start,
                    StartTimezone = task.StartTimezone,
                    EndTime = task.End,
                    EndTimezone = task.EndTimezone,
                    Description = task.Description,
                    RecurrenceRule = task.RecurrenceRule,
                    RecurrenceException = task.RecurrenceException,
                    RecurrenceID = task.RecurrenceID,
                    IsAllDay = task.IsAllDay,
                    EventTaskListId = task.OwnerID ?? 0
                };

                db.EventSchedules.Add(entity);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new[] { task });
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = task.TaskID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Task/5
        public HttpResponseMessage DeleteTask(EventViewModel deletedTask)
        {
            EventSchedule task = db.EventSchedules.SingleOrDefault(p => p.Id == deletedTask.TaskID);
            if (task == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.EventSchedules.Remove(task);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, task);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}