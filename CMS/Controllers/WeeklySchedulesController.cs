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
    public class WeeklySchedulesController : ApiController
    {
        private CmsContext db = new CmsContext();

        // GET: api/WeeklySchedules
        [Route("")]
        public IQueryable<WeeklySchedule> GetWeeklySchedules()
        {
            return db.WeeklySchedules;
        }

        // GET: api/WeeklySchedules/5
        [Route("{id:int}")]
        [ResponseType(typeof (WeeklySchedule))]
        public IHttpActionResult GetWeeklySchedule(int id)
        {
            WeeklySchedule weeklySchedule = db.WeeklySchedules.Find(id);
            if (weeklySchedule == null)
            {
                return NotFound();
            }

            return Ok(weeklySchedule);
        }

        [Route("~/api/crews/{crewId:int}/schedule")]
        [ResponseType(typeof (WeeklySchedule))]
        public IHttpActionResult GetCrewWeeklySchedule(int crewId)
        {
            var crew = db.Crews.FirstOrDefault(c => c.Id == crewId);
            if (crew == null)
            {
                return NotFound();
            }

            var weeklySchedule = new WeeklySchedule();// crew.WeeklySchedule;
            if (weeklySchedule == null)
            {
                return NotFound();
            }

            return Ok(weeklySchedule);
        }

        [Route("~/api/crews/{crewId:int}/schedule/{day}")]
        [ResponseType(typeof(WeeklySchedule))]
        public IHttpActionResult GetCrewDailySchedule(int crewId, string day)
        {
            var crew = db.Crews
                //.Include(c => c.WeeklySchedule.SundaySchedule)
                //.Include(c => c.WeeklySchedule.MondaySchedule)
                //.Include(c => c.WeeklySchedule.TuesdaySchedule)
                //.Include(c => c.WeeklySchedule.WednesdaySchedule)
                //.Include(c => c.WeeklySchedule.ThursdaySchedule)
                //.Include(c => c.WeeklySchedule.FridaySchedule)
                //.Include(c => c.WeeklySchedule.SaturdaySchedule)
                .FirstOrDefault(c => c.Id == crewId);
            if (crew == null)
            {
                return NotFound();
            }

            var weeklySchedule = db.WeeklySchedules.FirstOrDefault(w => true);//w.Id == crew.WeeklyScheduleId);
            if (weeklySchedule == null)
            {
                return NotFound();
            }

            var daily = GetDailyScheduleForName(weeklySchedule, day);
            if (daily == null)
            {
                return NotFound();
            }

            return Ok(daily);
        }

        private DailySchedule GetDailyScheduleForName(WeeklySchedule weeklySchedule, string day)
        {
            switch (day.ToLower())
            {
                case "sunday":
                    return db.DailySchedules.FirstOrDefault(d => d.Id == weeklySchedule.SundayScheduleId);
                case "monday":
                    return db.DailySchedules.FirstOrDefault(d => d.Id == weeklySchedule.MondayScheduleId);
                case "tuesday":
                    return db.DailySchedules.FirstOrDefault(d => d.Id == weeklySchedule.TuesdayScheduleId);
                case "wednesday":
                    return db.DailySchedules.FirstOrDefault(d => d.Id == weeklySchedule.WednesdayScheduleId);
                case "thursday":
                    return db.DailySchedules.FirstOrDefault(d => d.Id == weeklySchedule.ThursdayScheduleId);
                case "friday":
                    return db.DailySchedules.FirstOrDefault(d => d.Id == weeklySchedule.FridayScheduleId);
                case "saturday":
                    return db.DailySchedules.FirstOrDefault(d => d.Id == weeklySchedule.SaturdayScheduleId);
                default:
                    return null;
            }
        }

        // PUT: api/WeeklySchedules/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWeeklySchedule(int id, WeeklySchedule weeklySchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != weeklySchedule.Id)
            {
                return BadRequest();
            }

            db.Entry(weeklySchedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeeklyScheduleExists(id))
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

        // POST: api/WeeklySchedules
        [Route("")]
        [ResponseType(typeof(WeeklySchedule))]
        public IHttpActionResult PostWeeklySchedule(WeeklySchedule weeklySchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WeeklySchedules.Add(weeklySchedule);
            db.SaveChanges();

            return Ok(weeklySchedule);
        }

        // DELETE: api/WeeklySchedules/5
        [Route("{id:int}")]
        [ResponseType(typeof(WeeklySchedule))]
        public IHttpActionResult DeleteWeeklySchedule(int id)
        {
            WeeklySchedule weeklySchedule = db.WeeklySchedules.Find(id);
            if (weeklySchedule == null)
            {
                return NotFound();
            }

            db.WeeklySchedules.Remove(weeklySchedule);
            db.SaveChanges();

            return Ok(weeklySchedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WeeklyScheduleExists(int id)
        {
            return db.WeeklySchedules.Count(e => e.Id == id) > 0;
        }
    }
}