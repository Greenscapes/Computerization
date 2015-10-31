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
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Greenscapes.Data.Repositories.Interfaces;
using CMS.Mappers;

namespace CMS.Controllers
{
    [RoutePrefix("api/servicetemplates")]
    public class ServiceTemplatesController : ApiController
    {
        private readonly IServiceTemplateRepository db = new ServiceTemplateRepository();

        // GET: api/ServiceTemplates
        [Route("")]
        public IEnumerable<ServiceTemplateViewModel> GetServiceTemplates()
        {
            return db.GetServiceTemplates().MapTo<IEnumerable<ServiceTemplateViewModel>>();
        }

        // GET: api/ServiceTemplates/5
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceTemplateViewModel))]
        public IHttpActionResult GetServiceTemplate(int id)
        {
            var serviceTemplate = db.GetServiceTemplate(id).MapTo<ServiceTemplateViewModel>();
            if (serviceTemplate == null)
            {
                return NotFound();
            }

            return Ok(serviceTemplate);
        }

        // PUT: api/servicetemplates/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceTemplate(int id, ServiceTemplateViewModel serviceTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceTemplate.Id)
            {
                return BadRequest();
            }

            db.UpdateServiceTemplate(serviceTemplate.MapTo<ServiceTemplate>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/servicetemplates
        [Route("")]
        [ResponseType(typeof(ServiceTemplateViewModel))]
        public IHttpActionResult PostServiceTemplate(ServiceTemplateViewModel serviceTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateServiceTemplate(serviceTemplate.MapTo<ServiceTemplate>());

            return Ok(serviceTemplate);
        }

        // DELETE: api/servicetemplates/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteServiceTemplate(int id)
        {
            var success = db.DeleteServiceTemplate(id);
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