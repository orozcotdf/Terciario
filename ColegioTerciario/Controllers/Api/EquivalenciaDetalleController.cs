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
    public class EquivalenciaDetalleController : ApiController
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: api/EquivalenciaDetalle
        public IQueryable<Equivalencia_Detalle> GetEquivalencias_Detalles()
        {
            return db.Equivalencias_Detalles;
        }

        // GET: api/EquivalenciaDetalle/5
        [ResponseType(typeof(Equivalencia_Detalle))]
        public IHttpActionResult GetEquivalencia_Detalle(int id)
        {
            Equivalencia_Detalle e = db.Equivalencias_Detalles.SingleOrDefault(eq => eq.ID == id);
            var result = new
            {
                e.ID,
                e.EQUIVALENCIA_DETALLE_TIPO,
                e.EQUIVALENCIA_DETALLE_PROFESOR_ID,
                PERSONA_NOMBRE = e.EQUIVALENCIA_DETALLE_PROFESOR.PERSONA_NOMBRE + " " + e.EQUIVALENCIA_DETALLE_PROFESOR.PERSONA_APELLIDO,
                e.EQUIVALENCIA_DETALLE_MATERIA_ID,
                e.EQUIVALENCIA_DETALLE_MATERIA.MATERIA_NOMBRE,
                e.EQUIVALENCIA_COMENTARIO,   
            };
            if (e == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/EquivalenciaDetalle/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquivalencia_Detalle(int id, Equivalencia_Detalle equivalencia_Detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equivalencia_Detalle.ID)
            {
                return BadRequest();
            }

            db.Entry(equivalencia_Detalle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Equivalencia_DetalleExists(id))
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

        // POST: api/EquivalenciaDetalle
        [ResponseType(typeof(Equivalencia_Detalle))]
        public IHttpActionResult PostEquivalencia_Detalle(Equivalencia_Detalle equivalencia_Detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equivalencias_Detalles.Add(equivalencia_Detalle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equivalencia_Detalle.ID }, equivalencia_Detalle);
        }

        // DELETE: api/EquivalenciaDetalle/5
        [ResponseType(typeof(Equivalencia_Detalle))]
        public IHttpActionResult DeleteEquivalencia_Detalle(int id)
        {
            Equivalencia_Detalle equivalencia_Detalle = db.Equivalencias_Detalles.Find(id);
            if (equivalencia_Detalle == null)
            {
                return NotFound();
            }

            db.Equivalencias_Detalles.Remove(equivalencia_Detalle);
            db.SaveChanges();

            return Ok(equivalencia_Detalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Equivalencia_DetalleExists(int id)
        {
            return db.Equivalencias_Detalles.Count(e => e.ID == id) > 0;
        }
    }
}