using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Mappers;
using CMS.Models;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Greenscapes.Data.Repositories.Interfaces;

namespace CMS.Controllers
{
    [RoutePrefix("api/eventtasklists")]
    public class EventTaskListController : ApiController
    {
        private readonly IEventTaskListRepository db = new EventTaskListRepository();

        // GET: api/eventtasklists
        [Route("~/api/properties/{propertyId:int}/task/{taskId:int}/eventtasklists")]
        public IEnumerable<EventTaskListViewModel> GetEventTaskLists(int propertyId, int taskId)
        {
            return db.GetEventTaskLists(propertyId, taskId).MapTo<IEnumerable<EventTaskListViewModel>>();
        }

        [Route("~/api/properties/{propertyId:int}/eventtasklists")]
        public IEnumerable<EventTaskListViewModel> GetPropertyEventTaskLists(int propertyId)
        {
            return db.GetPropertyEventTaskLists(propertyId).MapTo<IEnumerable<EventTaskListViewModel>>();
        }

        // GET: api/eventtasklists/5
        [Route("{id:int}")]
        [ResponseType(typeof(EventTaskListViewModel))]
        public IHttpActionResult GetEventTaskList(int id)
        {
            var eventTaskList = db.GetEventTaskList(id).MapTo<EventTaskListViewModel>();
            if (eventTaskList == null)
            {
                return NotFound();
            }

            return Ok(eventTaskList);
        }

        [Route("{id:int}/start/{start:DateTime}"), HttpPut]
        public IHttpActionResult SetStartTime(int id, DateTime start)
        {
            db.SetStartTime(id, start);

            return Ok();
        }

        [Route("{id:int}/finish/{finish:DateTime}"), HttpPut]
        public IHttpActionResult SetFinishTime(int id, DateTime finish)
        {
            db.SetFinishTime(id, finish);

            return Ok();
        }

        [Route("")]
        public IHttpActionResult GetEventTaskLists()
        {
            var eventTaskLists = db.GetEventTaskLists().MapTo<List<EventTaskListViewModel>>();
            if (eventTaskLists == null)
            {
                return NotFound();
            }

            return Ok(eventTaskLists);
        }

        // PUT: api/EventTaskLists/5
        [Route("{id:int}")]
        [ResponseType(typeof(EventTaskListViewModel))]
        public IHttpActionResult PutEventTaskList(int id, EventTaskListViewModel eventTaskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateEventTaskList(eventTaskList.MapTo<EventTaskList>());

            return Ok(eventTaskList);
        }

        // POST: api/EventTaskLists
        [Route("")]
        [ResponseType(typeof(EventTaskListViewModel))]
        public IHttpActionResult PostEventTaskList(EventTaskListViewModel eventTaskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskList = db.UpdateEventTaskList(eventTaskList.MapTo<EventTaskList>()).MapTo<EventTaskListViewModel>();

            return Ok(taskList);
        }

        // DELETE: api/EventTaskLists/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteEventTaskList(int id)
        {
            var success = db.DeleteEventTaskList(id);
            if (!success)
            {
                return NotFound();
            }

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