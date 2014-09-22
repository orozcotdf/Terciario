using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;

namespace ColegioTerciario.Controllers
{
    public class PersonasController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Personas
        public ActionResult Index()
        {
            var personas = db.Personas.Include(p => p.PERSONA_NACIMIENTO_CIUDAD).Include(p => p.PERSONA_NACIMIENTO_PAIS).Include(p => p.PERSONA_NACIMIENTO_PROVINCIA);
            return View(personas.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.PERSONA_NACIMIENTO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME");
            ViewBag.PERSONA_NACIMIENTO_PAIS_ID = new SelectList(db.Paises, "ID", "PAIS_NAME");
            ViewBag.PERSONA_NACIMIENTO_PROVINCIA_ID = new SelectList(db.Provincias, "ID", "PROVINCIA_NAME_ASCII");
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PERSONA_CODIGO,PERSONA_USUARIO,PERSONA_CLAVE,PERSONA_NOMBRE,PERSONA_APELLIDO,PERSONA_DOCUMENTO_TIPO,PERSONA_DOCUMENTO_NUMERO,PERSONA_NACIMIENTO_FECHA,PERSONA_EMAIL,PERSONA_DOMICILIO,PERSONA_TELEFONO,PERSONA_SEXO,PERSONA_FECHA_ALTA,PERSONA_FECHA_BAJA,PERSONA_TITULO_SECUNDARIO,PERSONA_OBSERVACION,PERSONA_FOTO,PERSONA_CUIL,PERSONA_ES_ALUMNO,PERSONA_ES_DOCENTE,PERSONA_ES_NODOCENTE,PERSONA_NACIMIENTO_PAIS_ID,PERSONA_NACIMIENTO_PROVINCIA_ID,PERSONA_NACIMIENTO_CIUDAD_ID")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PERSONA_NACIMIENTO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME", persona.PERSONA_NACIMIENTO_CIUDAD_ID);
            ViewBag.PERSONA_NACIMIENTO_PAIS_ID = new SelectList(db.Paises, "ID", "PAIS_NAME", persona.PERSONA_NACIMIENTO_PAIS_ID);
            ViewBag.PERSONA_NACIMIENTO_PROVINCIA_ID = new SelectList(db.Provincias, "ID", "PROVINCIA_NAME_ASCII", persona.PERSONA_NACIMIENTO_PROVINCIA_ID);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.PERSONA_NACIMIENTO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME", persona.PERSONA_NACIMIENTO_CIUDAD_ID);
            ViewBag.PERSONA_NACIMIENTO_PAIS_ID = new SelectList(db.Paises, "ID", "PAIS_NAME", persona.PERSONA_NACIMIENTO_PAIS_ID);
            ViewBag.PERSONA_NACIMIENTO_PROVINCIA_ID = new SelectList(db.Provincias, "ID", "PROVINCIA_NAME_ASCII", persona.PERSONA_NACIMIENTO_PROVINCIA_ID);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PERSONA_CODIGO,PERSONA_USUARIO,PERSONA_CLAVE,PERSONA_NOMBRE,PERSONA_APELLIDO,PERSONA_DOCUMENTO_TIPO,PERSONA_DOCUMENTO_NUMERO,PERSONA_NACIMIENTO_FECHA,PERSONA_EMAIL,PERSONA_DOMICILIO,PERSONA_TELEFONO,PERSONA_SEXO,PERSONA_FECHA_ALTA,PERSONA_FECHA_BAJA,PERSONA_TITULO_SECUNDARIO,PERSONA_OBSERVACION,PERSONA_FOTO,PERSONA_CUIL,PERSONA_ES_ALUMNO,PERSONA_ES_DOCENTE,PERSONA_ES_NODOCENTE,PERSONA_NACIMIENTO_PAIS_ID,PERSONA_NACIMIENTO_PROVINCIA_ID,PERSONA_NACIMIENTO_CIUDAD_ID")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PERSONA_NACIMIENTO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME", persona.PERSONA_NACIMIENTO_CIUDAD_ID);
            ViewBag.PERSONA_NACIMIENTO_PAIS_ID = new SelectList(db.Paises, "ID", "PAIS_NAME", persona.PERSONA_NACIMIENTO_PAIS_ID);
            ViewBag.PERSONA_NACIMIENTO_PROVINCIA_ID = new SelectList(db.Provincias, "ID", "PROVINCIA_NAME_ASCII", persona.PERSONA_NACIMIENTO_PROVINCIA_ID);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Personas.Find(id);
            db.Personas.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Index");
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
