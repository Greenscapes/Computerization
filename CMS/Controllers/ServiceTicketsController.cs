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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMS.Controllers
{
    [RoutePrefix("api/servicetickets")]
    public class ServiceTicketsController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        [Route("manager")]
        public ManagerViewModel GetUnapprovedServiceTickets()
        {
            var serviceTickets = db.ServiceTickets.Include("EventTaskList.EventTaskTimes").Where(s => !s.ApprovedDate.HasValue);
            var managerViewModel = new ManagerViewModel();
            managerViewModel.UnapprovedTickets = new List<UnapprovedServiceTicketViewModel>();

            foreach (var serviceTicket in serviceTickets)
            {
                if (!serviceTicket.EventDate.HasValue || !serviceTicket.EventTaskListId.HasValue)
                    continue;

                var ticket = new UnapprovedServiceTicketViewModel();
                ticket.EventTaskListId = serviceTicket.EventTaskListId.Value;
                ticket.CrewName = serviceTicket.EventTaskList.Crew.Name;
                ticket.Title = serviceTicket.EventTaskList.Name;
                ticket.EventDate = serviceTicket.EventDate.Value;
                var property = serviceTicket.EventTaskList.Property;
                ticket.PropertyAddress = property.Address1 + " " + property.Address2 + " " + property.City.Name + " " + property.State + " " + property.Zip;
                var eventTaskTime = serviceTicket.EventTaskList.EventTaskTimes.FirstOrDefault(e => e.EventDate.Subtract(serviceTicket.EventDate.Value) == TimeSpan.Zero);
                if (eventTaskTime != null)
                {
                    ticket.TaskStartTime = eventTaskTime.StartTime;
                    ticket.TaskEndTime = eventTaskTime.EndTime;
                }

                managerViewModel.UnapprovedTickets.Add(ticket);
            }

            return managerViewModel;
        }

        [Route("manager/approved")]
        public ManagerViewModel GetApprovedServiceTickets()
        {
            var serviceTickets = db.ServiceTickets.Include("EventTaskList.EventTaskTimes").Where(s => s.ApprovedDate.HasValue);
            var managerViewModel = new ManagerViewModel();
            managerViewModel.ApprovedTickets = new List<UnapprovedServiceTicketViewModel>();

            foreach (var serviceTicket in serviceTickets)
            {
                if (!serviceTicket.EventDate.HasValue || !serviceTicket.EventTaskListId.HasValue)
                    continue;

                var ticket = new UnapprovedServiceTicketViewModel();
                ticket.EventTaskListId = serviceTicket.EventTaskListId.Value;
                ticket.CrewName = serviceTicket.EventTaskList.Crew.Name;
                ticket.Title = serviceTicket.EventTaskList.Name;
                ticket.EventDate = serviceTicket.EventDate.Value;
                var property = serviceTicket.EventTaskList.Property;
                ticket.PropertyAddress = property.Address1 + " " + property.Address2 + " " + property.City.Name + " " + property.State + " " + property.Zip;
                var eventTaskTime = serviceTicket.EventTaskList.EventTaskTimes.FirstOrDefault(e => e.EventDate.Subtract(serviceTicket.EventDate.Value) == TimeSpan.Zero);
                if (eventTaskTime != null)
                {
                    ticket.TaskStartTime = eventTaskTime.StartTime;
                    ticket.TaskEndTime = eventTaskTime.EndTime;
                }

                managerViewModel.ApprovedTickets.Add(ticket);
            }

            return managerViewModel;
        }

        // GET: api/ServiceTickets/5/2015-10-10
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceTicketViewModel))]
        public IHttpActionResult GetServiceTicket(int id)
        {
            var serviceTemplate = db.ServiceTemplates.FirstOrDefault(s => s.Id == id);
            var property = new Property()
            {
                Name = "Property Name",
                Address1 = "Address",
                City = new City(),
                State = "ST",
                Zip = "00000"
            };
            if (serviceTemplate == null || property == null)
            {
                return NotFound();
            }

            var serviceTicket = new ServiceTicket();

            serviceTicket.EventTaskListId = id;
            serviceTicket.EventDate = DateTime.Now;
            serviceTicket.VisitFromTime = DateTime.Today.AddHours(8);
            serviceTicket.VisitToTime = DateTime.Today.AddHours(10);
            serviceTicket.ServiceTemplateId = serviceTemplate.Id;
            serviceTicket.JsonFields = serviceTemplate.JsonFields;

            var serviceMembers = from e in db.Employees
                                 orderby e.FirstName, e.LastName
                                 select new ServiceMemberViewModel()
                                 {
                                     EmployeeId = e.Id,
                                     FirstName = e.FirstName,
                                     LastName = e.LastName,
                                     IsCrewMember = true,
                                     Selected = db.ServiceMembers.Any(s => s.EmployeeId == e.Id && s.ServiceTicketId == serviceTicket.Id)
                                 };

            var ticket = serviceTicket.MapTo<ServiceTicketViewModel>();
            ticket.TemplateName = serviceTemplate.Name;
            ticket.TemplateUseTasks = serviceTemplate.UseTasks;
            ticket.TemplateUrl = serviceTemplate.Url;
            ticket.PropertyName = property.Name;
            ticket.Address1 = property.Address1;
            ticket.Address2 = property.Address2;
            ticket.City = property.City != null ? property.City.Name : "";
            ticket.State = property.State;
            ticket.Zip = property.Zip;
            if (property.Customer != null)
            {
                ticket.AccessDetails = property.Customer.AccessDetails;
                ticket.CustomerName = property.Customer.FirstName + " " + property.Customer.LastName;
            }
            ticket.Members = serviceMembers.ToList();
            ticket.ShowAllEmployees = true;

            return Ok(ticket);
        }


        // GET: api/ServiceTickets/5/2015-10-10
        [Route("{id:int}/{date:DateTime}")]
        [ResponseType(typeof(ServiceTicketViewModel))]
        public IHttpActionResult GetServiceTicket(int id, DateTime date)
        {
            var eventTaskList = db.EventTaskLists.Include("EventTaskTimes").FirstOrDefault(e => e.Id == id);

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
                var eventTaskTime = eventTaskList.EventTaskTimes.FirstOrDefault(e => e.EventDate.Subtract(serviceTicket.EventDate.Value) == TimeSpan.Zero);
                if (eventTaskTime != null)
                {
                    serviceTicket.VisitFromTime = eventTaskTime.StartTime?.AddHours(4) ?? DateTime.Today.AddHours(8);
                    serviceTicket.VisitToTime = eventTaskTime.EndTime?.AddHours(4) ?? DateTime.Today.AddHours(10);
                }
                else
                {
                    serviceTicket.VisitFromTime = DateTime.Today.AddHours(8);
                    serviceTicket.VisitToTime = DateTime.Today.AddHours(10);
                }
                serviceTicket.ServiceTemplateId = serviceTemplate.Id;
                // if (serviceTemplate.UseTasks)
                // {
                var tasks = from t in db.PropertyTasks
                            where t.EventTaskListId == eventTaskList.Id
                            select new TaskInfo()
                            {
                                Description = t.Description,
                                Location = t.Location,
                                Completed = false,
                                Comment = string.Empty
                            };

                //   serviceTicket.JsonFields = "{\"Tasks\":" + JsonConvert.SerializeObject(tasks.ToList()) + "}";

                // }
                // else
                // {

                JObject json = JObject.Parse(serviceTemplate.JsonFields);
                JArray tasksArray = (JArray)json["Tasks"];
                if (!tasksArray.HasValues)
                {
                    foreach (var task in tasks)
                    {
                        tasksArray.Add(JObject.FromObject(task));
                    }
                }

                serviceTicket.JsonFields = JsonConvert.SerializeObject(json);

                //  }
                serviceTicket.ReferenceNumber = property.PropertyRefNumber;
                db.ServiceTickets.Add(serviceTicket);
                db.SaveChanges();
            }

            var serviceMembers = from e in db.Employees
                                 join c in db.CrewMembers on

                                  new
                                  {
                                      EmployeeId = e.Id,
                                      eventTaskList.CrewId
                                  } equals new
                                  {
                                      c.EmployeeId,
                                      c.CrewId
                                  }
                                 into cm
                                 from c in cm.DefaultIfEmpty()
                                 orderby e.FirstName, e.LastName
                                 select new ServiceMemberViewModel()
                                 {
                                     EmployeeId = e.Id,
                                     FirstName = e.FirstName,
                                     LastName = e.LastName,
                                     IsCrewMember = c != null,
                                     Selected = db.ServiceMembers.Any(s => s.EmployeeId == e.Id && s.ServiceTicketId == serviceTicket.Id)
                                 };

            var ticket = serviceTicket.MapTo<ServiceTicketViewModel>();
            ticket.TemplateName = serviceTemplate.Name;
            ticket.TemplateUseTasks = serviceTemplate.UseTasks;
            ticket.TemplateUrl = serviceTemplate.Url;
            ticket.PropertyName = property.Name;
            ticket.Address1 = property.Address1;
            ticket.Address2 = property.Address2;
            ticket.City = property.City != null ? property.City.Name : "";
            ticket.State = property.State;
            ticket.Zip = property.Zip;
            ticket.Members = serviceMembers.ToList();
            ticket.ShowAllEmployees = serviceMembers.Any(s => s.Selected && !s.IsCrewMember);
            if (property.Customer != null)
            {
                ticket.AccessDetails = property.Customer.AccessDetails;
                ticket.CustomerName = property.Customer.FirstName + " " + property.Customer.LastName;
            }
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

    public class TaskInfo
    {
        public string Description { get; set; }
        public string Location { get; set; }
        public bool Completed { get; set; }
        public string Comment { get; set; }
    }

}