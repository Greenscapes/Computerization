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
    [RoutePrefix("api/tasktemplates")]
    public class TaskTemplateController : ApiController
    {
        private readonly TaskTemplateRepository db = new TaskTemplateRepository();

        // GET: api/PropertyTasks
        [Route("{id:int}")]
        public TaskTemplateViewModel GetTaskTemplate(int id)
        {
            return db.GetTaskTemplate(id).MapTo<TaskTemplateViewModel>();
        }

        [Route("")]
        public IEnumerable<TaskTemplateViewModel> GetTaskTemplates()
        {
            return db.GetTaskTemplates().MapTo<IEnumerable<TaskTemplateViewModel>>();
        }

        // PUT: api/PropertyTasks/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskTemplate(int id, TaskTemplateViewModel propertyTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyTask.Id)
            {
                return BadRequest();
            }

            db.UpdateTaskTemplate(propertyTask.MapTo<TaskTemplate>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PropertyTasks
        [Route("")]
        [ResponseType(typeof(TaskTemplateViewModel))]
        public IHttpActionResult PostPropertyTask(TaskTemplateViewModel propertyTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.UpdateTaskTemplate(propertyTask.MapTo<TaskTemplate>());

            return Ok(propertyTask);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteTaskTemplate(int id)
        {
            var success = db.DeleteTaskTemplate(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
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