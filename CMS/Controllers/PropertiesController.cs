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
    [RoutePrefix("api/properties")]
    public class PropertiesController : ApiController
    {
        private readonly IPropertyRepository db = new PropertyRepository();
        private readonly ICustomerRepository customerRepository = new CustomerRepository();

        // GET: api/Properties
        [Route("")]
        public IEnumerable<PropertyViewModel> GetProperties()
        {
            return db.GetProperties().MapTo<IEnumerable<PropertyViewModel>>();
        }

        // GET: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult GetProperty(int id)
        {
            var property = db.GetProperty(id).MapTo<PropertyViewModel>();
            if (property == null)
            {
                return NotFound();
            }

            if (property.CustomerId.HasValue)
            {
                var customer = customerRepository.GetCustomer(property.CustomerId.Value);
                if (customer != null)
                {
                    property.CustomerName = customer.FirstName + " " + customer.LastName;
                }
            }         

            return Ok(property);
        }

        [Route("getNextReference")]
        public string GetNextReferenceNumber()
        {
            var properties = db.GetProperties().OrderByDescending(p => p.PropertyRefNumber);
            var refNumber = properties.First().PropertyRefNumber;
            var number = Convert.ToInt32(refNumber.Replace("RM", ""));

            return "RM" + (number + 1).ToString("0000");
        }

        // PUT: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, PropertyViewModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.Id)
            {
                return BadRequest();
            }

            db.UpdateProperty(property.MapTo<Property>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [Route("")]
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult PostProperty(PropertyViewModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateProperty(property.MapTo<Property>());

            return Ok(property);
        }

        // DELETE: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteProperty(int id)
        {
            var success = db.DeleteProperty(id);
            if (!success)
            {
                return NotFound();
            }

            //var taskLists = property.PropertyTaskLists.ToList();
            //taskLists.ForEach(l =>
            //{
            //    var tasks = l.PropertyTasks.ToList();
            //    tasks.ForEach(t =>
            //    {
            //        t.PropertyTaskDetails.ToList().ForEach(d => db.PropertyTaskDetails.Remove(d));

            //        t.EventSchedules.ToList().ForEach(e =>
            //        {
            //            db.EventSchedules.Remove(e);
            //        });
            //        db.PropertyTasks.Remove(t);
            //    });
            //    db.PropertyTaskLists.Remove(l);
            //});

            //db.Properties.Remove(property);
            //db.SaveChanges();

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