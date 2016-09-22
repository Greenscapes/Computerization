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
    [RoutePrefix("api/cities")]
    public class CitiesController : ApiController
    {
        private readonly ICityRepository db = new CityRepository();

        // GET: api/Properties
        [Route("")]
        public IEnumerable<CityViewModel> GetCities()
        {
            return db.GetCities().MapTo<IEnumerable<CityViewModel>>();
        }

        // GET: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(CityViewModel))]
        public IHttpActionResult GetProperty(int id)
        {
            var city = db.GetCity(id).MapTo<CityViewModel>();
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCity(int id, CityViewModel city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.Id)
            {
                return BadRequest();
            }

            db.UpdateCity(city.MapTo<City>());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [Route("")]
        [ResponseType(typeof(CityViewModel))]
        public IHttpActionResult PostCity(CityViewModel city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UpdateCity(city.MapTo<City>());

            return Ok(city);
        }

        // DELETE: api/Properties/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteCity(int id)
        {
            var success = db.DeleteCity(id);
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