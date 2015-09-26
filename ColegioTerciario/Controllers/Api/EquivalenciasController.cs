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
using ColegioTerciario.Models.ViewModels;
using ColegioTerciario.Models.ViewModels.Api;

namespace ColegioTerciario.Controllers.Api
{
    public class EquivalenciasController : ApiController
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: api/Equivalencias
        public AjaxCollectionResponseViewModel GetEquivalencias([FromUri] AjaxCollectionParamViewModel param)
        {
            IQueryable<EquivalenciaViewModel> evm = db.Equivalencias
                .OrderByDescending(e => e.ID)
                .Skip(param.Pagina * param.RegistrosPorPagina)
                .Take(param.RegistrosPorPagina)
                .Select(e => new EquivalenciaViewModel
                {
                    ID = e.ID,
                    EQUIVALENCIA_FECHA = e.EQUIVALENCIA_FECHA,
                    EQUIVALENCIA_NRO_DISPOSICION = e.EQUIVALENCIA_NRO_DISPOSICION,
                    EQUIVALENCIA_ALUMNO_NOMBRE = e.EQUIVALENCIA_ALUMNO.PERSONA_NOMBRE + " " + e.EQUIVALENCIA_ALUMNO.PERSONA_APELLIDO,
                    EQUIVALENCIA_CARRERA_NOMBRE = e.EQUIVALENCIA_CARRERA.CARRERA_NOMBRE
                });
            AjaxCollectionResponseViewModel rvm = new AjaxCollectionResponseViewModel
            {
                Resultados = evm,
                CantidadResultados = db.Equivalencias.Count(),
            };
            return rvm;
        }

        // GET: api/Equivalencias/5
        [ResponseType(typeof(EquivalenciaViewModel))]
        public IHttpActionResult GetEquivalencia(int id)
        {
            EquivalenciaViewModel equivalencia = (from e in db.Equivalencias
                                        where (e.ID == id)
                                        select new EquivalenciaViewModel
                                        {   
                                            ID = e.ID,
                                            EQUIVALENCIA_FECHA = e.EQUIVALENCIA_FECHA,
                                            EQUIVALENCIA_NRO_DISPOSICION = e.EQUIVALENCIA_NRO_DISPOSICION,
                                            EQUIVALENCIA_ALUMNO_ID = e.EQUIVALENCIA_ALUMNO_ID,
                                            EQUIVALENCIA_CARRERA_ID = e.EQUIVALENCIA_CARRERA_ID,
                                            EQUIVALENCIA_ALUMNO_NOMBRE = e.EQUIVALENCIA_ALUMNO.PERSONA_NOMBRE + " " + e.EQUIVALENCIA_ALUMNO.PERSONA_APELLIDO,
                                            EQUIVALENCIA_CARRERA_NOMBRE = e.EQUIVALENCIA_CARRERA.CARRERA_NOMBRE
                                        }).SingleOrDefault();
            if (equivalencia == null)
            {
                return NotFound();
            }

            return Ok(equivalencia);
        }

        // PUT: api/Equivalencias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquivalencia(int id, Equivalencia equivalencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equivalencia.ID)
            {
                return BadRequest();
            }

            db.Entry(equivalencia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquivalenciaExists(id))
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

        // POST: api/Equivalencias
        [ResponseType(typeof(Equivalencia))]
        public IHttpActionResult PostEquivalencia(Equivalencia equivalencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equivalencias.Add(equivalencia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equivalencia.ID }, equivalencia);
        }

        [HttpPost]
        public IHttpActionResult AgregaMateria(Equivalencia_Detalle equivalenciaDetalle)
        {
            db.Equivalencias_Detalles.Add(equivalenciaDetalle);
            db.SaveChanges();
            return Ok(equivalenciaDetalle);
        }

        // DELETE: api/Equivalencias/5
        [ResponseType(typeof(Equivalencia))]
        public IHttpActionResult DeleteEquivalencia(int id)
        {
            Equivalencia equivalencia = db.Equivalencias.Find(id);
            if (equivalencia == null)
            {
                return NotFound();
            }

            db.Equivalencias.Remove(equivalencia);
            db.SaveChanges();

            return Ok(equivalencia);
        }

        public AjaxCollectionResponseViewModel GetDetalles(int id, [FromUri] AjaxCollectionParamViewModel param)
        {
            var detalles = db.Equivalencias_Detalles
                .Where(e => e.EQUIVALENCIA_ID == id)
                .Take(param.RegistrosPorPagina)
                .Select(e =>
                new
                {
                    e.ID,
                    e.EQUIVALENCIA_DETALLE_TIPO,
                    e.EQUIVALENCIA_DETALLE_PROFESOR_ID,
                    e.EQUIVALENCIA_DETALLE_PROFESOR.PERSONA_NOMBRE,
                    e.EQUIVALENCIA_DETALLE_MATERIA_ID,
                    e.EQUIVALENCIA_DETALLE_MATERIA.MATERIA_NOMBRE,
                    e.EQUIVALENCIA_COMENTARIO,             
                }
                );
            AjaxCollectionResponseViewModel rvm = new AjaxCollectionResponseViewModel
            {
                Resultados = detalles,
                CantidadResultados = db.Equivalencias_Detalles.Count(),
            };
            return rvm;
        }


        [ResponseType(typeof(Equivalencia_Detalle))]
        [HttpPost]
        public IHttpActionResult AgregaEquivalencia(int id, Equivalencia_Detalle eq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            eq.EQUIVALENCIA_ID = id;

            db.Equivalencias_Detalles.Add(eq);
            db.SaveChanges();
            return Ok(eq);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquivalenciaExists(int id)
        {
            return db.Equivalencias.Count(e => e.ID == id) > 0;
        }
    }
}