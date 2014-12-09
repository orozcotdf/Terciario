using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Lib;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;

namespace ColegioTerciario.Controllers.Api
{
    public class ActasController : ApiController
    {
        private readonly ColegioTerciarioContext _db = new ColegioTerciarioContext();

        [HttpGet]
        public object GetActas([FromUri]DataTableParamModel param)
        {
            var actas = _db.Actas_Examenes
                    .Include(a => a.ACTA_EXAMEN_TURNO_EXAMEN.TURNO_EXAMEN_CICLO)
                    .Include(a => a.ACTA_EXAMEN_MATERIA)
                    .Include(a => a.ACTA_EXAMEN_CARRERA)
                    .Include(a => a.ACTA_EXAMEN_PRESIDENTE)
                    .Include(a => a.ACTA_EXAMEN_VOCAL1)
                    .Include(a => a.ACTA_EXAMEN_VOCAL2);


            // Busqueda
            IEnumerable<Acta_Examen> actasFiltradas = actas.Where(
                c => param.sSearch == null || 
                    c.ACTA_EXAMEN_CARRERA.CARRERA_NOMBRE.ToLower().Contains(param.sSearch.ToLower()) ||
                    c.ACTA_EXAMEN_MATERIA.MATERIA_NOMBRE.ToLower().Contains(param.sSearch.ToLower()) ||
                    c.ACTA_EXAMEN_FOLIO.ToLower().Contains(param.sSearch.ToLower()) ||
                    c.ACTA_EXAMEN_LIBRO.ToLower().Contains(param.sSearch.ToLower())
                );
            

            //Filtrado
            var sortColumnIndex = Convert.ToInt32(HttpContext.Current.Request.Params.Get("iSortCol_0"));
            
            Func<Acta_Examen, object> orderingFunction = (
                c => sortColumnIndex == 1 ? c.ACTA_EXAMEN_FECHA : 
                     sortColumnIndex == 2 ? c.ACTA_EXAMEN_LIBRO : 
                     sortColumnIndex == 3 ? c.ACTA_EXAMEN_FOLIO.ToNullableInt32() :
                     sortColumnIndex == 4 ? c.ACTA_EXAMEN_TURNO_EXAMEN.TURNO_EXAMEN_NOMBRE :
                     sortColumnIndex == 5 ? c.ACTA_EXAMEN_CARRERA.CARRERA_NOMBRE :
                     sortColumnIndex == 6 ? (object) c.ACTA_EXAMEN_MATERIA.MATERIA_NOMBRE : c.ACTA_EXAMEN_FECHA

                                                                );
            
            var sortDirection = HttpContext.Current.Request.Params.Get("sSortDir_0"); // asc or desc
            
            if (sortDirection == "asc")
                actasFiltradas = actasFiltradas.OrderBy(orderingFunction);
            else
                actasFiltradas = actasFiltradas.OrderByDescending(orderingFunction);
            
                
            //Paginacion
            var result = from c in actasFiltradas.Skip(param.iDisplayStart)
                         .Take(param.iDisplayLength)
                         select new ActasDataTablesViewModel() {
                             ID = c.ID,
                             Fecha = string.Format("{0:dd/MM/yyyy}", c.ACTA_EXAMEN_FECHA),
                             Libro = c.ACTA_EXAMEN_LIBRO,
                             Folio = c.ACTA_EXAMEN_FOLIO,
                             Turno = c.ACTA_EXAMEN_TURNO_EXAMEN != null ? c.ACTA_EXAMEN_TURNO_EXAMEN.TURNO_EXAMEN_NOMBRE : null,
                             Carrera = c.ACTA_EXAMEN_CARRERA != null ? c.ACTA_EXAMEN_CARRERA.CARRERA_NOMBRE : null,                       
                             Materia = c.ACTA_EXAMEN_MATERIA != null ? c.ACTA_EXAMEN_MATERIA.MATERIA_NOMBRE : null,
                             Presidente = c.ACTA_EXAMEN_PRESIDENTE != null ? c.ACTA_EXAMEN_PRESIDENTE.PERSONA_NOMBRE : null,
                             Vocal1 = c.ACTA_EXAMEN_VOCAL1 != null ? c.ACTA_EXAMEN_VOCAL1.PERSONA_NOMBRE : null,
                             Vocal2 = c.ACTA_EXAMEN_VOCAL2 != null ? c.ACTA_EXAMEN_VOCAL2.PERSONA_NOMBRE : null,
                         };

            return new
            {
                sEcho = param.sEcho,
                iTotalRecords = actas.Count(),
                iTotalDisplayRecords = actasFiltradas.Count(),
                iDisplayStart = param.iDisplayStart,
                iDisplayLength = param.iDisplayLength,
                aaData = result
            };
            
        }

        // GET: api/Actas/5
        [ResponseType(typeof(Acta_Examen_Detalle))]
        public IHttpActionResult GetActa_Examen_Detalle(int id)
        {
            Acta_Examen_Detalle acta_Examen_Detalle = _db.Actas_Examenes_Detalles.Find(id);
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

            _db.Entry(acta_Examen_Detalle).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
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

            _db.Actas_Examenes_Detalles.Add(acta_Examen_Detalle);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = acta_Examen_Detalle.ID }, acta_Examen_Detalle);
        }

        // DELETE: api/Actas/5
        [ResponseType(typeof(Acta_Examen_Detalle))]
        [HttpDelete]
        public IHttpActionResult EliminarIntegrante(int id)
        {
            Acta_Examen_Detalle acta_Examen_Detalle = _db.Actas_Examenes_Detalles.Find(id);
            if (acta_Examen_Detalle == null)
            {
                return NotFound();
            }

            _db.Actas_Examenes_Detalles.Remove(acta_Examen_Detalle);
            _db.SaveChanges();

            return Ok(acta_Examen_Detalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Acta_Examen_DetalleExists(int id)
        {
            return _db.Actas_Examenes_Detalles.Count(e => e.ID == id) > 0;
        }
    }
}