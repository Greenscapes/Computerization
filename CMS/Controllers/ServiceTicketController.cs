using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;
using System;

namespace CMS.Controllers
{
    [RoutePrefix("api/servicetickets")]
    public class ServiceTicketController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/servicetickets
        [Route("")]
        public IQueryable<ServiceTicket> GetServiceTickets()
        {
            return null;
        }

        // GET: api/servicetickets/5
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceTicket))]
        public IHttpActionResult GetServiceTicket(int id)
        {
            ServiceTicket ticket = new ServiceTicket();

            ticket.Id = id;
            ticket.TemplateName = "Irrigation Installation";
            ticket.TemplateUrl = "/templates/servicetickets/irrigation-installation.html";
            ticket.ReferenceNumber = "AB100223";
            ticket.JsonFields = "{ \"ZoneNumber\": 10, \"hello\":\"function(){ alert('hi'); }\" }";

            return Ok(ticket);
        }

        // PUT: api/servicetickets/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceTicket(int id, ServiceTicket serviceTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceTicket.Id)
            {
                return BadRequest();
            }

            db.Entry(serviceTicket).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceTicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/servicetickets
        [Route("")]
        [ResponseType(typeof(ServiceTicket))]
        public IHttpActionResult PostServiceTicket(ServiceTicket serviceTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SaveChanges();

            return Ok(serviceTicket);
        }

        // DELETE: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceTicket))]
        public IHttpActionResult DeleteServiceTicket(int id)
        {
            var serviceTicket = db.Properties.Find(id);
            if (serviceTicket == null)
            {
                return NotFound();
            }

            db.SaveChanges();

            return Ok(serviceTicket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceTicketExists(int id)
        {
            return db.Properties.Count(e => e.Id == id) > 0;
        }
    }
}