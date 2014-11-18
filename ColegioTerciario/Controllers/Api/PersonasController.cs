using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http.Description;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;
using System.Web.Http;

namespace ColegioTerciario.Controllers.Api
{
    public class PersonasController : ApiController
    {
        private readonly ColegioTerciarioContext _db = new ColegioTerciarioContext();

        // GET: api/Personas
        [HttpGet]
        public object GetPersonas([FromUri]DataTableParamModel param)
        {
            var personas = _db.Personas.AsQueryable();
            IQueryable<Persona> personasFiltradas;

            if (param.sSearch == null)
            {
                personasFiltradas = personas;
            }
            else
            {
                personasFiltradas = (from e in personas
                                     where (
                                     e.PERSONA_DOCUMENTO_NUMERO.ToLower().Contains(param.sSearch.ToLower()) ||
                                     e.PERSONA_NOMBRE.ToLower().Contains(param.sSearch.ToLower()) ||
                                     e.PERSONA_APELLIDO.ToLower().Contains(param.sSearch.ToLower()))
                                     select e);
            }
            var result = personasFiltradas.Select(p => new{p.PERSONA_NOMBRE, p.PERSONA_APELLIDO, p.ID, p.PERSONA_DOCUMENTO_NUMERO})
                            .OrderBy(p => p.PERSONA_APELLIDO)
                            .Skip(param.iDisplayStart)
                            .Take(param.iDisplayLength);
            return new
            {
                sEcho = param.sEcho,
                iTotalRecords = personas.Count(),
                iTotalDisplayRecords = personasFiltradas.Count(),
                iDisplayStart = param.iDisplayStart,
                iDisplayLength = param.iDisplayLength,
                aaData = result
            };
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(int id)
        {
            Persona persona = _db.Personas.Find(id);
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

            _db.Entry(persona).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
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

            _db.Personas.Add(persona);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = persona.ID }, persona);
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(int id)
        {
            Persona persona = _db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            _db.Personas.Remove(persona);
            _db.SaveChanges();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(int id)
        {
            return _db.Personas.Count(e => e.ID == id) > 0;
        }
    }
}