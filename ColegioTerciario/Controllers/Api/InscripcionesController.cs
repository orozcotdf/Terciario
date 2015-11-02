using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using ColegioTerciario.DAL.Models.Inscripciones;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;
using RazorEngine;
using SendGrid;
using InscripcionesViewModel = ColegioTerciario.Models.ViewModels.Api.InscripcionesViewModel;

using RazorEngine.Templating;

namespace ColegioTerciario.Controllers.Api
{
    public class InscripcionesController : ApiController
    {
        private const string MailUsername = "azure_fd22ccb1747e880c2d79095cced78667@azure.com";
        private const string MailPassword = "DgIYom7LOfwZs3x";
        private const string MailHost = "smtp.sendgrid.net";
        private const string MailApiKey = "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";

        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: api/Inscripciones
        public IQueryable<Inscripciones> GetInscripciones()
        {
            return db.Inscripciones;
        }

        // GET: api/Inscripciones/5
        [ResponseType(typeof(Inscripciones))]
        public IHttpActionResult GetInscripciones(Guid id)
        {
            Inscripciones inscripciones = db.Inscripciones.Find(id);
            if (inscripciones == null)
            {
                return NotFound();
            }

            return Ok(inscripciones);
        }

        [HttpGet]
        public IQueryable<ReactSelectViewModel> GetCarrerasHabilitadas()
        {
            /*
            var carreras = db.Inscripciones_Carrera.Where(
                c => c.CARRERA_NOMBRE.ToLower().Contains(busqueda.ToLower()) 
                    && DbFunctions.TruncateTime(c.CARRERA_HABILITADA_DESDE) <= DateTime.Today 
                    && DbFunctions.TruncateTime(c.CARRERA_HABILITADA_HASTA) >= DateTime.Today)
                .Select(c => new ReactSelectViewModel
                {
                    label = c.CARRERA_NOMBRE,
                    value = c.ID.ToString()
                }).Take(5);
            */
            var carreras = db.Inscripciones_Carrera.Where(
                c => DbFunctions.TruncateTime(c.CARRERA_HABILITADA_DESDE) <= DateTime.Today
                    && DbFunctions.TruncateTime(c.CARRERA_HABILITADA_HASTA) >= DateTime.Today)
                .Select(c => new ReactSelectViewModel
                {
                    label = c.CARRERA_NOMBRE,
                    value = c.ID.ToString()
                });
            return carreras;
        }

        // PUT: api/Inscripciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInscripciones(Guid id, Inscripciones inscripciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inscripciones.ID)
            {
                return BadRequest();
            }

            db.Entry(inscripciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionesExists(id))
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

        // POST: api/Inscripciones
        [ResponseType(typeof(Inscripciones))]
        public IHttpActionResult PostInscripciones(InscripcionesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (
                db.Inscripciones.Count(e => e.INSCRIPCIONES_DOCUMENTO_NUMERO.Equals(vm.INSCRIPCIONES_DOCUMENTO_NUMERO)) >
                0)
            {
                return BadRequest("Usted ya esta inscripto");
            }


            Inscripciones nuevaInscripcion = new Inscripciones
            {
                INSCRIPCIONES_APELLIDO = vm.INSCRIPCIONES_APELLIDO,
                INSCRIPCIONES_NOMBRE = vm.INSCRIPCIONES_NOMBRE,
                INSCRIPCIONES_SEXO = vm.INSCRIPCIONES_SEXO,
                INSCRIPCIONES_TELEFONO = vm.INSCRIPCIONES_TELEFONO,
                INSCRIPCIONES_EMAIL = vm.INSCRIPCIONES_EMAIL,
                INSCRIPCIONES_DOCUMENTO_TIPO = vm.INSCRIPCIONES_DOCUMENTO_TIPO,
                INSCRIPCIONES_DOCUMENTO_NUMERO = vm.INSCRIPCIONES_DOCUMENTO_NUMERO,
                INSCRIPCIONES_ES_ALUMNO = true,
                INSCRIPCIONES_NACIMIENTO_CIUDAD_ID = vm.INSCRIPCIONES_NACIMIENTO_CIUDAD_ID,
                INSCRIPCIONES_NACIMIENTO_BARRIO_ID = vm.INSCRIPCIONES_NACIMIENTO_BARRIO_ID,
                INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID = vm.INSCRIPCIONES_NACIMIENTO_PROVINCIA_ID,
                INSCRIPCIONES_NACIMIENTO_PAIS_ID = vm.INSCRIPCIONES_NACIMIENTO_PAIS_ID,
                INSCRIPCIONES_NACIMIENTO_FECHA = vm.INSCRIPCIONES_NACIMIENTO_FECHA,
                INSCRIPCIONES_DOMICILIO = vm.INSCRIPCIONES_DOMICILIO,
                INSCRIPCIONES_TITULO_SECUNDARIO = vm.INSCRIPCIONES_TITULO_SECUNDARIO,
                INSCRIPCIONES_CARRERA_ID = vm.INSCRIPCIONES_CARRERA_ID
            };
            /*
            if (db.Inscripciones.Any())
            {
                nuevaInscripcion.INSCRIPCIONES_IDENTIFICADOR = 1;
            }
            else
            {
                nuevaInscripcion.INSCRIPCIONES_IDENTIFICADOR = db.Inscripciones.Last().INSCRIPCIONES_IDENTIFICADOR + 1;
            }
            */
            db.Inscripciones.Add(nuevaInscripcion);
            db.SaveChanges();

            // Preparar template html
            

            string templateFile = "/Areas/Publico/Views/Inscripciones/mailTemplate.cshtml";
            //var html = Engine.Razor.RunCompile(new LoadedTemplateSource(template, templateFile), "key", null, new { id = nuevaInscripcion.ID});
            var template = File.ReadAllText(HostingEnvironment.MapPath(templateFile));
            
            SendGridMessage mensaje = new SendGridMessage();
            var url = Url.Route("Publico_default", new { controller = "Inscripciones", action = "ImprimirInscripcion", id = nuevaInscripcion.ID });
            var urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);

            var html = Engine.Razor.RunCompile(template, nuevaInscripcion.ID.ToString() , null, new { URL = urlBase + url });

            mensaje.AddTo(vm.INSCRIPCIONES_EMAIL);
            mensaje.From = new MailAddress("administracion@cent11.edu.ar", "Administracion Cent11");
            mensaje.Subject = "Formulario de Inscripcion";
            mensaje.Text = String.Format("<a href='{0}'>Haga click aqui para ver el formulario</a>", urlBase + url);
            mensaje.Html = html;
            // mensaje.EnableClickTracking(true);

            // Create an Web transport for sending email.
            var transportWeb = new Web(MailApiKey);

            // Send the email, which returns an awaitable task.
            transportWeb.DeliverAsync(mensaje);

            return CreatedAtRoute("DefaultApi", new { id = nuevaInscripcion.ID }, nuevaInscripcion);
        }

        // DELETE: api/Inscripciones/5
        [ResponseType(typeof(Inscripciones))]
        public IHttpActionResult DeleteInscripciones(Guid id)
        {
            Inscripciones inscripciones = db.Inscripciones.Find(id);
            if (inscripciones == null)
            {
                return NotFound();
            }

            db.Inscripciones.Remove(inscripciones);
            db.SaveChanges();

            return Ok(inscripciones);
        }

        [HttpGet]
        public IHttpActionResult VerificarHabilitacion(string dni)
        {
            bool existe = db.Inscripciones.Count(e => e.INSCRIPCIONES_DOCUMENTO_NUMERO.Equals(dni)) > 0;
            if (existe)
                // Ya se inscribio
                return NotFound();
            else
                // Puede inscribirse
                return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InscripcionesExists(Guid id)
        {
            return db.Inscripciones.Count(e => e.ID == id) > 0;
        }
    }
}