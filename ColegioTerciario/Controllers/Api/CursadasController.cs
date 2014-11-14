using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;

namespace ColegioTerciario.Controllers.Api
{
    public class CursadasController : ApiController
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: api/Cursadas
        public IQueryable<Cursada> GetCursadas()
        {
            return db.Cursadas;
        }

        // GET: api/Cursadas/5
        [ResponseType(typeof(Cursada))]
        public IHttpActionResult GetCursada(int id)
        {
            Cursada cursada = db.Cursadas.Find(id);
            if (cursada == null)
            {
                return NotFound();
            }

            return Ok(cursada);
        }

        // PUT: api/Cursadas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCursada(int id, Cursada cursada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cursada.ID)
            {
                return BadRequest();
            }

            db.Entry(cursada).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursadaExists(id))
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

        // POST: api/Cursadas
        [ResponseType(typeof(Cursada))]
        public IHttpActionResult PostCursada(Cursada cursada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cursadas.Add(cursada);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cursada.ID }, cursada);
        }

        // DELETE: api/Cursadas/5
        [ResponseType(typeof(Cursada))]
        public IHttpActionResult DeleteCursada(int id)
        {
            Cursada cursada = db.Cursadas.Find(id);
            if (cursada == null)
            {
                return NotFound();
            }

            db.Cursadas.Remove(cursada);
            db.SaveChanges();

            return Ok(cursada);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursadaExists(int id)
        {
            return db.Cursadas.Count(e => e.ID == id) > 0;
        }
    }
}