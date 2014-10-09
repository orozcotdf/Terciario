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
    public class ActaExamenController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: ActaExamen
        public ActionResult Index()
        {
            var actas_examenes = db.Actas_Examenes
                .Include(a => a.ACTA_EXAMEN_TURNO_EXAMEN.TURNO_EXAMEN_CICLO)
                .Include(a => a.ACTA_EXAMEN_MATERIA)
                .Include(a => a.ACTA_EXAMEN_CARRERA)
                .Include(a => a.ACTA_EXAMEN_PRESIDENTE)
                .Include(a => a.ACTA_EXAMEN_VOCAL1)
                .Include(a => a.ACTA_EXAMEN_VOCAL2)
                .ToList();
            return View(actas_examenes);
        }

        // GET: ActaExamen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acta_Examen acta_Examen = db.Actas_Examenes.Find(id);
            if (acta_Examen == null)
            {
                return HttpNotFound();
            }
            return View(acta_Examen);
        }

        // GET: ActaExamen/Create
        public ActionResult Create()
        {
            ViewBag.CARRERAS = new SelectList(db.Carreras, "ID", "CARRERA_NOMBRE");
            ViewBag.MATERIAS = new SelectList(db.Materias, "ID", "MATERIA_NOMBRE");
            ViewBag.PERSONAS = new SelectList(db.Personas, "ID", "PERSONA_NOMBRE");
            ViewBag.TURNOS = new SelectList(db.Turnos_Examenes.Include(t => t.TURNO_EXAMEN_CICLO), "ID", "TURNO_EXAMEN_NOMBRE_PARA_MOSTRAR");
            return View();
        }

        // POST: ActaExamen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ACTA_EXAMEN_FECHA,ACTA_EXAMEN_LIBRO,ACTA_EXAMEN_FOLIO,ACTA_EXAMEN_TURNOS_EXAMENES_ID,ACTA_EXAMEN_CARRERAS_ID,ACTA_EXAMEN_MATERIAS_ID,ACTA_EXAMEN_PRESIDENTE_ID,ACTA_EXAMEN_VOCAL1_ID,ACTA_EXAMEN_VOCAL2_ID")] Acta_Examen acta_Examen)
        {
            if (ModelState.IsValid)
            {
                db.Actas_Examenes.Add(acta_Examen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acta_Examen);
        }

        // GET: ActaExamen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acta_Examen acta_Examen = db.Actas_Examenes.Find(id);

            if (acta_Examen == null)
            {
                return HttpNotFound();
            }
            ViewBag.CARRERAS = new SelectList(db.Carreras, "ID", "CARRERA_NOMBRE");
            ViewBag.MATERIAS = new SelectList(db.Materias, "ID", "MATERIA_NOMBRE");
            ViewBag.PERSONAS = new SelectList(db.Personas, "ID", "PERSONA_NOMBRE");
            ViewBag.TURNOS = new SelectList(db.Turnos_Examenes.Include(t => t.TURNO_EXAMEN_CICLO), "ID", "TURNO_EXAMEN_NOMBRE_PARA_MOSTRAR");
            return View(acta_Examen);
        }

        // POST: ActaExamen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ACTA_EXAMEN_FECHA,ACTA_EXAMEN_INSCRIPTOS,ACTA_EXAMEN_APROBADOS,ACTA_EXAMEN_REPROBADOS,ACTA_EXAMEN_AUSENTES,ACTA_EXAMEN_LIBRO,ACTA_EXAMEN_FOLIO,ACTA_EXAMEN_TURNOS_EXAMENES_ID,ACTA_EXAMEN_CARRERAS_ID,ACTA_EXAMEN_MATERIAS_ID,ACTA_EXAMEN_PRESIDENTE_ID,ACTA_EXAMEN_VOCAL1_ID,ACTA_EXAMEN_VOCAL2_ID")] Acta_Examen acta_Examen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acta_Examen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acta_Examen);
        }

        // GET: ActaExamen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acta_Examen acta_Examen = db.Actas_Examenes.Find(id);
            if (acta_Examen == null)
            {
                return HttpNotFound();
            }
            return View(acta_Examen);
        }

        // POST: ActaExamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Acta_Examen acta_Examen = db.Actas_Examenes.Find(id);
            db.Actas_Examenes.Remove(acta_Examen);
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
