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
    [RoutePrefix("api/holidays")]
    public class HolidaysController : ApiController
    {
        private readonly IHolidayRepository db = new HolidayRepository();

        // GET: api/Properties
        [Route("")]
        public IEnumerable<HolidayViewModel> GetHolidays()
        {
            return db.GetHolidays().MapTo<IEnumerable<HolidayViewModel>>();
        }

        // GET: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(HolidayViewModel))]
        public IHttpActionResult GetHoliday(int id)
        {
            var holiday = db.GetHoliday(id).MapTo<HolidayViewModel>();
            if (holiday == null)
            {
                return NotFound();
            }

            return Ok(holiday);
        }

        // PUT: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHoliday(int id, HolidayViewModel holiday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != holiday.Id)
            {
                return BadRequest();
            }

            db.UpdateHoliday(holiday.MapTo<Holiday>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [Route("")]
        [ResponseType(typeof(HolidayViewModel))]
        public IHttpActionResult PostHoliday(HolidayViewModel holiday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateHoliday(holiday.MapTo<Holiday>());

            return Ok(holiday);
        }

        // DELETE: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteHoliday(int id)
        {
            var success = db.DeleteHoliday(id);
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