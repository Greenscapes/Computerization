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
        private readonly IServiceTicketRepository serviceTicketRepo = new ServiceTicketRepository();
        private readonly IEventTaskListRepository eventTaskListRepo = new EventTaskListRepository();
        private readonly IServiceTemplateRepository serviceTemplateRepo = new ServiceTemplateRepository();
        private readonly IPropertyRepository propertyRepo = new PropertyRepository();
        
        // GET: api/ServiceTickets/5/2015-10-10
        [Route("{id:int}/{date:DateTime}")]
        [ResponseType(typeof(ServiceTicketViewModel))]
        public IHttpActionResult GetServiceTicket(int id, DateTime date)
        {
            var serviceTicket = serviceTicketRepo.GetServiceTicket(id, date);
            var eventTaskList = eventTaskListRepo.GetEventTaskList(id);
            
            if (eventTaskList == null || !eventTaskList.ServiceTemplateId.HasValue)
            {
                return NotFound();
            }

            var serviceTemplate = serviceTemplateRepo.GetServiceTemplate(eventTaskList.ServiceTemplateId.Value);

            if (serviceTicket == null)
            {
                serviceTicket = new ServiceTicket();
                serviceTicket.EventTaskListId = id;
                serviceTicket.EventDate = date;
                serviceTicket.VisitFromTime = DateTime.Now.Date;
                serviceTicket.ServiceTemplateId = serviceTemplate.Id;
                serviceTicket.JsonFields = serviceTemplate.JsonFields;
                serviceTicketRepo.UpdateServiceTicket(serviceTicket);
            }

            var property = propertyRepo.GetProperty(eventTaskList.PropertyId);

            var ticket = serviceTicket.MapTo<ServiceTicketViewModel>();
            ticket.TemplateName = serviceTemplate.Name;
            ticket.TemplateUrl = serviceTemplate.Url;
            ticket.PropertyName = property.Name;
            ticket.Address1 = property.Address1;
            ticket.Address2 = property.Address2;
            ticket.City = property.City;
            ticket.State = property.State;
            ticket.Zip = property.Zip;
            
            return Ok(ticket);
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

            serviceTicketRepo.UpdateServiceTicket(serviceTicket.MapTo<ServiceTicket>());

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

            serviceTicketRepo.UpdateServiceTicket(serviceTicket.MapTo<ServiceTicket>());

            return Ok(serviceTicket);
        }

        // DELETE: api/servicetickets/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteServiceTicket(int id)
        {
            var success = serviceTicketRepo.DeleteServiceTicket(id);
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
                serviceTicketRepo.Dispose();
                eventTaskListRepo.Dispose();
                serviceTemplateRepo.Dispose();
                propertyRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}