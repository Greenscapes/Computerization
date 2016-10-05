using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Mappers;
using CMS.Models;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Greenscapes.Data.Repositories.Interfaces;

namespace CMS.Controllers
{
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {
        private readonly IServiceRepository serviceRepository = new ServiceRepository();

        [Route("~/api/properties/{propertyId:int}/services")]
        public IEnumerable<PropertyServiceViewModel> GetServicesForProperty(int propertyId)
        {
            return serviceRepository.GetPropertyServices(propertyId).MapTo<IEnumerable<PropertyServiceViewModel>>();
        }

        [Route("")]
        public IEnumerable<ServiceViewModel> GetServices()
        {
            return serviceRepository.GetServices().MapTo<IEnumerable<ServiceViewModel>>();
        }

        // GET: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceViewModel))]
        public IHttpActionResult GetService(int id)
        {
            var service = serviceRepository.GetService(id).MapTo<ServiceViewModel>();
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(int id, ServiceViewModel service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            serviceRepository.UpdateService(service.MapTo<Service>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [Route("")]
        [ResponseType(typeof(ServiceViewModel))]
        public IHttpActionResult PostService(ServiceViewModel service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            serviceRepository.UpdateService(service.MapTo<Service>());

            return Ok(service);
        }

        // DELETE: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteService(int id)
        {
            var success = serviceRepository.DeleteService(id);
            if (!success)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("properties/{id:int}")]
        [ResponseType(typeof(List<PropertyServiceViewModel>))]
        public IHttpActionResult GetPropertyServices(int id)
        {
            var service = serviceRepository.GetPropertyServices(id).MapTo<List<PropertyServiceViewModel>>();
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // GET: api/Properties/5
        [Route("property/{id:int}")]
        [ResponseType(typeof(PropertyServiceViewModel))]
        public IHttpActionResult GetPropertyService(int id)
        {
            var service = serviceRepository.GetPropertyService(id).MapTo<PropertyServiceViewModel>();
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Properties/5
        [Route("property/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyService(int id, PropertyServiceViewModel service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            serviceRepository.UpdatePropertyService(service.MapTo<PropertyService>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [Route("property")]
        [ResponseType(typeof(PropertyServiceViewModel))]
        public IHttpActionResult PostPropertyService(PropertyServiceViewModel service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            serviceRepository.UpdatePropertyService(service.MapTo<PropertyService>());

            return Ok(service);
        }

        // DELETE: api/Properties/5
        [Route("property/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeletePropertyService(int id)
        {
            var success = serviceRepository.DeletePropertyService(id);
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
                serviceRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}