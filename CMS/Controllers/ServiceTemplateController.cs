using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using CMS.Models;
using System;

namespace CMS.Controllers
{
    [RoutePrefix("api/servicetemplates")]
    public class ServiceTemplatesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        private static List<ServiceTemplate> ServiceTemplates = new List<ServiceTemplate>();

        static ServiceTemplatesController()
        {
            ServiceTemplates.Add(new ServiceTemplate() { Id = 1, Name = "Arborjet Treatment", Url = "/templates/servicetickets/arborjet-treatment.html", JsonFields = "{\"Trees\":[],\"DefaultTree\":{\"Kind\":\"Palm\",\"Type\":\"Coconut\"}}" });
        }


        // GET: api/servicetemplates
        [Route("")]
        public List<ServiceTemplate> GetServiceTemplates()
        {
            return ServiceTemplates;
        }

        // GET: api/servicetemplates/5
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceTemplate))]
        public IHttpActionResult GetServiceTemplate(int id)
        {
            var serviceTemplate = ServiceTemplates.Find(s => s.Id == id);
            if (serviceTemplate == null)
            {
                return NotFound();
            }
            
            return Ok(serviceTemplate);
        }

        // PUT: api/servicetemplates/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceTemplate(int id, ServiceTemplate serviceTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceTemplate.Id)
            {
                return BadRequest();
            }

            var originalServiceTemplate = ServiceTemplates.Find(s => s.Id == id);

            if (originalServiceTemplate == null)
                return NotFound();

            originalServiceTemplate.Name = serviceTemplate.Name;
            originalServiceTemplate.Url = serviceTemplate.Url;
            originalServiceTemplate.JsonFields = serviceTemplate.JsonFields;
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/servicetemplates
        [Route("")]
        [ResponseType(typeof(ServiceTemplate))]
        public IHttpActionResult PostServiceTemplate(ServiceTemplate serviceTemplate)
        {
            serviceTemplate.Id = DateTime.Now.Millisecond;

            ServiceTemplates.Add(serviceTemplate);

            return Ok(serviceTemplate);
        }

        // DELETE: api/servicetemplates/5
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceTemplate))]
        public IHttpActionResult DeleteServiceTemplate(int id)
        {
            ServiceTemplate serviceTemplate = ServiceTemplates.Find(s => s.Id == id);
                
            if (serviceTemplate == null)
            {
                return NotFound();
            }

            ServiceTemplates.Remove(serviceTemplate);

            return Ok(serviceTemplate);
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