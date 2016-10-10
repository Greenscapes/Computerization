using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper.Internal;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Greenscapes.Data.Repositories.Interfaces;
using CMS.Mappers;
using DDay.iCal;

namespace CMS.Controllers
{
    [RoutePrefix("api/properties")]
    public class PropertiesController : ApiController
    {
        private readonly IPropertyRepository db = new PropertyRepository();
        private readonly ICustomerRepository customerRepository = new CustomerRepository();
        [Route("")]
        public IEnumerable<PropertyViewModel> GetProperties()
        {
            var properties = db.GetProperties();
            return properties.MapTo<IEnumerable<PropertyViewModel>>();
        }
        [Route("type/{type:int}")]
        public IEnumerable<PropertyViewModel> GetProperties(int type)
        {
            var properties = db.GetProperties().Where(p=>p.PropertyType==type).OrderBy(p=>p.Name);
            return properties.MapTo<IEnumerable<PropertyViewModel>>();
        }
         

        // GET: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult GetProperty(int id)
        {
            var property = db.GetProperty(id).MapTo<PropertyViewModel>();
            if (property == null)
            {
                return NotFound();
            }

            if (property.CustomerId.HasValue)
            {
                var customer = customerRepository.GetCustomer(property.CustomerId.Value);
                if (customer != null)
                {
                    property.CustomerName = customer.FirstName + " " + customer.LastName;
                }
            }         

            return Ok(property);
        }

        [Route("{id:int}/schedule")]
        [ResponseType(typeof(List<EventTaskListScheduleViewModel>))]
        public IHttpActionResult GetPropertySchedule(int id)
        {
            var repository = new EventTaskListRepository();
            var taskLists = repository.GetPropertyEventTaskLists(id);
            var eventTaskListSchedules = new List<EventTaskListScheduleViewModel>();

            foreach (var taskList in taskLists)
            {
                var eventTaskListSchedule = new EventTaskListScheduleViewModel();
                eventTaskListSchedule.Id = taskList.Id;
                eventTaskListSchedule.Name = taskList.Name;
                eventTaskListSchedule.EventCount = taskList.PropertyService == null ? 0 : taskList.PropertyService.EventCount;
                eventTaskListSchedule.Schedules = new List<EventScheduleViewModel>();
                decimal idx = 0;
                foreach (var eventSchedule in taskList.EventSchedules)
                {
                    if (!string.IsNullOrEmpty(eventSchedule.RecurrenceRule))
                    {
                        var pattern = new RecurrencePattern(eventSchedule.RecurrenceRule);
                        var evaluator = new RecurrencePatternEvaluator(pattern);
                        var items = evaluator.Evaluate(new iCalDateTime(eventSchedule.StartTime), eventSchedule.StartTime,
                            eventSchedule.EndTime, false);
                        foreach(var item in items)
                        {
                            idx = idx + taskList.NumberServices ?? 0;
                            var schedule = new EventScheduleViewModel();
                            schedule.Date = item.StartTime.ToString("d");
                            schedule.Time = item.StartTime.ToString("t");
                            schedule.DateTime = item.StartTime.Value;
                            schedule.EventNumber = idx;
                            schedule.FreeService = taskList.FreeServices.Any(f => f.ServiceTime == item.StartTime.Value);
                            eventTaskListSchedule.Schedules.Add(schedule);
                        }
                    }
                    else
                    {
                        idx = idx + taskList.NumberServices ?? 0;
                        var schedule = new EventScheduleViewModel();
                        schedule.Date = eventSchedule.StartTime.ToString("d");
                        schedule.Time = eventSchedule.StartTime.ToString("t");
                        schedule.DateTime = eventSchedule.StartTime;
                        schedule.EventNumber = idx;
                        schedule.FreeService = taskList.FreeServices.Any(f => f.ServiceTime == eventSchedule.StartTime);
                        eventTaskListSchedule.Schedules.Add(schedule);
                    }
                }

                if (eventTaskListSchedule.Schedules.Any() || eventTaskListSchedule.EventCount > 0)
                {
                    eventTaskListSchedules.Add(eventTaskListSchedule);
                }
            }

            return Ok(eventTaskListSchedules);
        }

        [Route("setFreeService"), HttpPut]
        [ResponseType(typeof(void))]
        public void SetFreeService(FreeServiceUpdate freeServiceUpdate)
        {
            using (var repo = new EventScheduleRepository())
            {
                repo.SetFreeService(freeServiceUpdate.PropertyId, freeServiceUpdate.EventTaskListId,
                    freeServiceUpdate.IsFreeService, freeServiceUpdate.ServiceDate);
            }

        }

        [Route("getNextReference")]
        public string GetNextReferenceNumber()
        {
            var properties = db.GetProperties().OrderByDescending(p => p.PropertyRefNumber);
            var refNumber = properties.First().PropertyRefNumber;
            var number = Convert.ToInt32(refNumber.Replace("RM", ""));

            return "RM" + (number + 1).ToString("0000");
        }

        // PUT: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, PropertyViewModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.Id)
            {
                return BadRequest();
            }

            db.UpdateProperty(property.MapTo<Property>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [Route("")]
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult PostProperty(PropertyViewModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateProperty(property.MapTo<Property>());

            return Ok(property);
        }

        // DELETE: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteProperty(int id)
        {
            var success = db.DeleteProperty(id);
            if (!success)
            {
                return NotFound();
            }

            //var taskLists = property.PropertyTaskLists.ToList();
            //taskLists.ForEach(l =>
            //{
            //    var tasks = l.PropertyTasks.ToList();
            //    tasks.ForEach(t =>
            //    {
            //        t.PropertyTaskDetails.ToList().ForEach(d => db.PropertyTaskDetails.Remove(d));

            //        t.EventSchedules.ToList().ForEach(e =>
            //        {
            //            db.EventSchedules.Remove(e);
            //        });
            //        db.PropertyTasks.Remove(t);
            //    });
            //    db.PropertyTaskLists.Remove(l);
            //});

            //db.Properties.Remove(property);
            //db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class FreeServiceUpdate
    {
        public bool IsFreeService { get; set; }
        public DateTime ServiceDate { get; set; }
        public int EventTaskListId { get; set; }
        public int PropertyId { get; set; }
    }
}