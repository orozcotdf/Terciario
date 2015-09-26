using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;

namespace ColegioTerciario.Controllers.Api
{
    public class CarrerasController : ApiController
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: api/Carreras
        public IQueryable<Carrera> GetCarreras()
        {
            return db.Carreras;
        }

        [HttpGet]
        public IQueryable<ReactSelectViewModel> SelectCarreras([FromUri]string busqueda)
        {
            return (from c in db.Carreras
                where (
                    c.CARRERA_NOMBRE.ToLower().Contains(busqueda.ToLower()) ||
                    c.CARRERA_NOMBRE_CORTO.ToLower().Contains(busqueda.ToLower())
                    )
                
                select new ReactSelectViewModel
                {
                    label = c.CARRERA_NOMBRE,
                    value = c.ID.ToString()
                }).Take(5);
        }

        // GET: api/Carreras/5
        [ResponseType(typeof(Carrera))]
        public IHttpActionResult GetCarrera(int id)
        {
            Carrera carrera = db.Carreras.Find(id);
            if (carrera == null)
            {
                return NotFound();
            }

            return Ok(carrera);
        }

        // PUT: api/Carreras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarrera(int id, Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carrera.ID)
            {
                return BadRequest();
            }

            db.Entry(carrera).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
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

        // POST: api/Carreras
        [ResponseType(typeof(Carrera))]
        public IHttpActionResult PostCarrera(Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carreras.Add(carrera);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carrera.ID }, carrera);
        }

        // DELETE: api/Carreras/5
        [ResponseType(typeof(Carrera))]
        public IHttpActionResult DeleteCarrera(int id)
        {
            Carrera carrera = db.Carreras.Find(id);
            if (carrera == null)
            {
                return NotFound();
            }

            db.Carreras.Remove(carrera);
            db.SaveChanges();

            return Ok(carrera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarreraExists(int id)
        {
            return db.Carreras.Count(e => e.ID == id) > 0;
        }
    }
}