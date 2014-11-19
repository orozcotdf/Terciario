using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;

namespace ColegioTerciario.Controllers.Api
{
    public class ActasController : ApiController
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: api/Actas
        public IQueryable<Acta_Examen_Detalle> GetActas_Examenes_Detalles()
        {
            return db.Actas_Examenes_Detalles;
        }

        // GET: api/Actas/5
        [ResponseType(typeof(Acta_Examen_Detalle))]
        public IHttpActionResult GetActa_Examen_Detalle(int id)
        {
            Acta_Examen_Detalle acta_Examen_Detalle = db.Actas_Examenes_Detalles.Find(id);
            if (acta_Examen_Detalle == null)
            {
                return NotFound();
            }

            return Ok(acta_Examen_Detalle);
        }

        // PUT: api/Actas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActa_Examen_Detalle(int id, Acta_Examen_Detalle acta_Examen_Detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != acta_Examen_Detalle.ID)
            {
                return BadRequest();
            }

            db.Entry(acta_Examen_Detalle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Acta_Examen_DetalleExists(id))
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

        // POST: api/Actas
        [ResponseType(typeof(Acta_Examen_Detalle))]
        public IHttpActionResult PostActa_Examen_Detalle(Acta_Examen_Detalle acta_Examen_Detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Actas_Examenes_Detalles.Add(acta_Examen_Detalle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = acta_Examen_Detalle.ID }, acta_Examen_Detalle);
        }

        // DELETE: api/Actas/5
        [ResponseType(typeof(Acta_Examen_Detalle))]
        [HttpDelete]
        public IHttpActionResult EliminarIntegrante(int id)
        {
            Acta_Examen_Detalle acta_Examen_Detalle = db.Actas_Examenes_Detalles.Find(id);
            if (acta_Examen_Detalle == null)
            {
                return NotFound();
            }

            db.Actas_Examenes_Detalles.Remove(acta_Examen_Detalle);
            db.SaveChanges();

            return Ok(acta_Examen_Detalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Acta_Examen_DetalleExists(int id)
        {
            return db.Actas_Examenes_Detalles.Count(e => e.ID == id) > 0;
        }
    }
}