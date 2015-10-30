using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using System.Collections.Generic;
using CMS.Mappers;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;

namespace CMS.Controllers
{
    [RoutePrefix("api/tasks")]
    public class PropertyTasksController : ApiController
    {
        private readonly PropertyTaskRepository db = new PropertyTaskRepository();

        // GET: api/PropertyTasks
        [Route("~/api/tasklists/{taskListId:int}/tasks")]
        public IEnumerable<PropertyTaskViewModel> GetPropertyTasksForList(int taskListId)
        {
            return db.GetPropertyTasksForTaskList(taskListId).MapTo<IEnumerable<PropertyTaskViewModel>>();
        }

        [Route("~/api/properties/{propertyId:int}/tasks")]
        public IEnumerable<PropertyTaskViewModel> GetPropertyTasks(int propertyId)
        {
            return db.GetPropertyTasksForProperty(propertyId).MapTo<IEnumerable<PropertyTaskViewModel>>();
        }

        // GET: api/PropertyTasks/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTaskViewModel))]
        public IHttpActionResult GetPropertyTask(int id)
        {
            var propertyTask = db.GetPropertyTask(id).MapTo<PropertyTaskViewModel>();
            if (propertyTask == null)
            {
                return NotFound();
            }

            return Ok(propertyTask);
        }

        // PUT: api/PropertyTasks/5
        [Route("{id:int}")]
        [Route("~/api/tasklists/{taskListId:int}/tasks/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyTask(int id, PropertyTaskViewModel propertyTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTask.Id)
            {
                return BadRequest();
            }

            db.UpdatePropertyTask(propertyTask.MapTo<PropertyTask>());

       //     foreach (var eventSchedule in propertyTask.EventSchedules)
       //     {
       //         eventSchedule.PropertyTaskId = propertyTask.Id;
       //         if (eventSchedule.Id > 0)
       //         {
                
       //             db.Entry(eventSchedule).State = EntityState.Modified;
       //         }
       //         else
       //         {
                   
       //             db.EventSchedules.Add(eventSchedule);
       //         }
       //     }
       //     //Remove events from property task
       //     List<EventSchedule> currentEvents = db.EventSchedules.Where(e => e.PropertyTaskId == id).ToList();

       //     foreach (EventSchedule eventsch in currentEvents)
       //     {
       //         EventSchedule eventSchValue = propertyTask.EventSchedules.SingleOrDefault(e => e.Id == eventsch.Id);
                                 
       //         if (eventSchValue == null)
       //         { 
       //             db.EventSchedules.Remove(eventsch);
       //         }
                
       //     }
            
       ////     db.Entry(propertyTask).State = EntityState.Modified;

       //     try
       //     {
       //         db.SaveChanges();
       //     }
       //     catch (DbUpdateConcurrencyException)
       //     {
       //         if (!PropertyTaskExists(id))
       //         {
       //             return NotFound();
       //         }
       //         else
       //         {
       //             throw;
       //         }
       //     }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PropertyTasks
        [Route("")]
        [Route("~/api/tasks/:taskListId/tasks")]
        [ResponseType(typeof(PropertyTaskViewModel))]
        public IHttpActionResult PostPropertyTask(PropertyTaskViewModel propertyTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.UpdatePropertyTask(propertyTask.MapTo<PropertyTask>());

            //List<Crew> crews= new List<Crew>();

            //foreach (Crew cr in propertyTask.Crews)
            //{
            //    crews.Add(db.Crews.Single(c=>c.Id==cr.Id));
            //}
            //propertyTask.Crews = crews;
            //db.PropertyTasks.Add(propertyTask);      
            //db.SaveChanges();

            return Ok(propertyTask);
        }

        // DELETE: api/PropertyTasks/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyTask))]
        public IHttpActionResult DeletePropertyTask(int id)
        {
            var success = db.DeletePropertyTask(id);
            //var propertyTask = db.PropertyTasks.Find(id);
            if (!success)
            {
                return NotFound();
            }
            //propertyTask.EventSchedules.ToList().ForEach(e => db.EventSchedules.Remove(e));

            //propertyTask.PropertyTaskDetails.ToList().ForEach(d => db.PropertyTaskDetails.Remove(d));
            //db.PropertyTasks.Remove(propertyTask);
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
}