using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Mappers;
using CMS.Models;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories;
using Greenscapes.Data.Repositories.Interfaces;

namespace CMS.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerRepository repository = new CustomerRepository();

        // GET: api/Properties
        [System.Web.Mvc.Route("")]
        public IEnumerable<CustomerViewModel> GetCustomers()
        {
            return repository.GetCustomers().MapTo<IEnumerable<CustomerViewModel>>();
        }

        // GET: api/Properties/5
        [System.Web.Mvc.Route("{id:int}")]
        [ResponseType(typeof(CustomerViewModel))]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = repository.GetCustomer(id).MapTo<CustomerViewModel>();
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Properties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.UpdateCustomer(customer.MapTo<Customer>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [System.Web.Mvc.Route("")]
        [ResponseType(typeof(CustomerViewModel))]
        public IHttpActionResult PostCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.UpdateCustomer(customer.MapTo<Customer>());

            return Ok(customer);
        }

        // DELETE: api/Properties/5
        [System.Web.Mvc.Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var success = repository.DeleteCustomer(id);
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
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
