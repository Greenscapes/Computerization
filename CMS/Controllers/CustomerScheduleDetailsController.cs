using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CMS.Models;

namespace CMS.Controllers
{
    [RoutePrefix("api/schedules")]
    public class CustomerScheduleDetailsController : ApiController
    {
        private readonly CmsContext db = new CmsContext();

        // GET: api/schedules
        [Route("")]
        public IQueryable<Property> GetProperties()
        {
            return db.Properties;
        }

       

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.Id == id) > 0;
        }
    }
}