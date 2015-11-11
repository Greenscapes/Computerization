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
using System.Linq;

namespace CMS.Controllers
{
    [RoutePrefix("api/servicetickets")]
    public class ServiceTicketsController : ApiController
    {
        private readonly CmsContext db = new CmsContext();
                
        // GET: api/ServiceTickets/5/2015-10-10
        [Route("{id:int}/{date:DateTime}")]
        [ResponseType(typeof(ServiceTicketViewModel))]
        public IHttpActionResult GetServiceTicket(int id, DateTime date)
        {
            var eventTaskList = db.EventTaskLists.FirstOrDefault(e => e.Id == id);
            
            if (eventTaskList == null || !eventTaskList.ServiceTemplateId.HasValue)
            {
                return NotFound();
            }

            var serviceTemplate = db.ServiceTemplates.FirstOrDefault(s => s.Id == eventTaskList.ServiceTemplateId);
            var property = db.Properties.FirstOrDefault(p => p.Id == eventTaskList.PropertyId);

            if (serviceTemplate == null || property == null)
            {
                return NotFound();
            }

            var serviceTicket = db.ServiceTickets.FirstOrDefault(s => s.EventTaskListId == id && s.EventDate == date);

            if (serviceTicket == null)
            {
                serviceTicket = new ServiceTicket();
                serviceTicket.EventTaskListId = id;
                serviceTicket.EventDate = date;
                serviceTicket.VisitFromTime = DateTime.Today.AddHours(8);
                serviceTicket.VisitToTime = DateTime.Today.AddHours(10);
                serviceTicket.ServiceTemplateId = serviceTemplate.Id;
                serviceTicket.JsonFields = serviceTemplate.JsonFields;
                serviceTicket.ReferenceNumber = property.PropertyRefNumber;
                db.ServiceTickets.Add(serviceTicket);
                db.SaveChanges();
            }

            var serviceMembers = from e in db.Employees
                               join c in db.CrewMembers on e.Id equals c.EmployeeId
                               where c.CrewId == eventTaskList.CrewId
                               orderby e.FirstName, e.LastName
                               select new ServiceMemberViewModel()
                               {
                                   EmployeeId = e.Id,
                                   FirstName = e.FirstName,
                                   LastName = e.LastName,
                                   Selected = db.ServiceMembers.Any(s => s.EmployeeId == e.Id && s.ServiceTicketId == serviceTicket.Id)
                               };

            var ticket = serviceTicket.MapTo<ServiceTicketViewModel>();
            ticket.TemplateName = serviceTemplate.Name;
            ticket.TemplateUrl = serviceTemplate.Url;
            ticket.PropertyName = property.Name;
            ticket.Address1 = property.Address1;
            ticket.Address2 = property.Address2;
            ticket.City = property.City;
            ticket.State = property.State;
            ticket.Zip = property.Zip;
            ticket.Members = serviceMembers.ToList();

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

            var existingTicket = db.ServiceTickets.FirstOrDefault(p => p.Id == id);
            if (existingTicket == null)
            { 
                return NotFound();
            }

            existingTicket.ServiceTemplateId = serviceTicket.ServiceTemplateId;
            existingTicket.EventTaskListId = serviceTicket.EventTaskListId;
            existingTicket.EventDate = serviceTicket.EventDate;
            existingTicket.ReferenceNumber = serviceTicket.ReferenceNumber;
            existingTicket.VisitFromTime = serviceTicket.VisitFromTime.Value.ToLocalTime();
            existingTicket.VisitToTime = serviceTicket.VisitToTime.Value.ToLocalTime();
            existingTicket.JsonFields = serviceTicket.JsonFields;
            existingTicket.ApprovedById = serviceTicket.ApprovedById;
            existingTicket.ApprovedDate = serviceTicket.ApprovedDate;
            existingTicket.Condition = serviceTicket.Condition;
            existingTicket.Notes = serviceTicket.Notes;

            db.ServiceMembers.RemoveRange(db.ServiceMembers.Where(s => s.ServiceTicketId == serviceTicket.Id));

            foreach (var member in serviceTicket.Members)
            {
                if (member.Selected)
                    db.ServiceMembers.Add(new ServiceMember() { ServiceTicketId = serviceTicket.Id, EmployeeId = member.EmployeeId });
            }
                        
            db.SaveChanges();

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

            db.ServiceTickets.Add(serviceTicket.MapTo<ServiceTicket>());
            db.SaveChanges();

            return Ok(serviceTicket);
        }

        // DELETE: api/servicetickets/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteServiceTicket(int id)
        {
            var serviceTicket = db.ServiceTickets.FirstOrDefault(p => p.Id == id);
            if (serviceTicket == null)
            {
                return NotFound();
            }

            db.ServiceMembers.RemoveRange(db.ServiceMembers.Where(s => s.ServiceTicketId == id));
            db.ServiceTickets.Remove(serviceTicket);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}