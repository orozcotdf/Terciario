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
using System.Web.Mvc;

namespace ColegioTerciario.Controllers.Api
{
    public class PersonasController : ApiController
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: api/Personas
        //[HttpGet]
        public object GetPersonas([FromUri]string sSearch, [FromUri] string sEcho, [FromUri]int iDisplayStart, [FromUri]int iDisplayLength)
        {
            var personas = db.Personas.AsQueryable();
            IQueryable<Persona> personasFiltradas;

            if (sSearch == null)
            {
                personasFiltradas = personas;
            }
            else
            {
                personasFiltradas = (from e in personas
                                     where (
                                     e.PERSONA_DOCUMENTO_NUMERO.ToLower().Contains(sSearch.ToLower()) ||
                                     e.PERSONA_NOMBRE.ToLower().Contains(sSearch.ToLower()) ||
                                     e.PERSONA_APELLIDO.ToLower().Contains(sSearch.ToLower()))
                                     select e);
            }
            var result = personasFiltradas
                            .OrderBy(p => p.PERSONA_APELLIDO)
                            .Skip(iDisplayStart)
                            .Take(iDisplayLength);
            var json = new
            {
                sEcho = sEcho,
                iTotalRecords = personas.Count(),
                iTotalDisplayRecords = personasFiltradas.Count(),
                iDisplayStart = iDisplayStart,
                iDisplayLength = iDisplayLength,
                aaData = result
            };
            return json;
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(int id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/Personas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersona(int id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.ID)
            {
                return BadRequest();
            }

            db.Entry(persona).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // POST: api/Personas
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personas.Add(persona);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = persona.ID }, persona);
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(int id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.Personas.Remove(persona);
            db.SaveChanges();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(int id)
        {
            return db.Personas.Count(e => e.ID == id) > 0;
        }
    }
}