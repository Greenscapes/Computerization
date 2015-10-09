using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/customerroutes")]
    public class CustomersRoutesController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/object
        [Route("")]
        public IQueryable<object> GetCustomersRoutes()
        {
            return null;
        }


        // GET: api/object/5
        [Route("{id:int}")]
      
        [ResponseType(typeof(object))]
        public IHttpActionResult GetCustomersRoute(int id)
        {
          
            return Ok(id);
        }

        // PUT: api/CrewMembers/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomersRoute(int id, object customersRoute)
        {
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CrewMembers
        [Route("")]
        [ResponseType(typeof(CrewMember))]
        public IHttpActionResult PostCustomersRoute(object customersRoute)
        {


            return Ok(customersRoute);
        }

        // DELETE: api/CrewMembers/5
        [Route("{id:int}")]
        [ResponseType(typeof(CrewMember))]
        public IHttpActionResult DeleteCustomersRoute(int id)
        {
           
            return Ok(id);
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