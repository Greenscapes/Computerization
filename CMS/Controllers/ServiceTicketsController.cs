using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using System.Collections.Generic;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Greenscapes.Data.Repositories.Interfaces;
using CMS.Mappers;

namespace CMS.Controllers
{
    [RoutePrefix("api/servicetickets")]
    public class ServiceTicketsController : ApiController
    {
        private readonly IServiceTicketRepository db = new ServiceTicketRepository();
        
        // GET: api/ServiceTickets/5
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceTicketViewModel))]
        public IHttpActionResult GetServiceTicket(int id)
        {
            var serviceTicketEntity = db.GetServiceTicket(id);
            var serviceTicket = serviceTicketEntity.MapTo<ServiceTicketViewModel>();

            if (serviceTicket == null)
            {
                return NotFound();
            }

            serviceTicket.TemplateName = serviceTicketEntity.ServiceTemplate.Name;
            serviceTicket.TemplateUrl = serviceTicketEntity.ServiceTemplate.Url;

            return Ok(serviceTicket);
        }

        // PUT: api/servicetickets/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceTicket(int id, ServiceTicketViewModel serviceTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceTicket.Id)
            {
                return BadRequest();
            }

            db.UpdateServiceTicket(serviceTicket.MapTo<ServiceTicket>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/servicetickets
        [Route("")]
        [ResponseType(typeof(ServiceTicketViewModel))]
        public IHttpActionResult PostServiceTicket(ServiceTicketViewModel serviceTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateServiceTicket(serviceTicket.MapTo<ServiceTicket>());

            return Ok(serviceTicket);
        }

        // DELETE: api/servicetickets/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteServiceTicket(int id)
        {
            var success = db.DeleteServiceTicket(id);
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