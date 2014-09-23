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
    public class MatriculasController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Matriculas
        public ActionResult Index()
        {
            var matriculas = db.Matriculas.Include(m => m.MATRICULA_ALUMNO).Include(m => m.MATRICULA_CARRERA).Include(m => m.MATRICULA_CICLO);
            return View(matriculas.ToList());
        }

        // GET: Matriculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // GET: Matriculas/Create
        public ActionResult Create()
        {
            ViewBag.MATRICULA_PERSONAS_ID = new SelectList(db.Personas, "ID", "PERSONA_CODIGO");
            ViewBag.MATRICULA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO");
            ViewBag.MATRICULA_CICLOS_ID = new SelectList(db.Ciclos, "ID", "CICLO_NOMBRE");
            return View();
        }

        // POST: Matriculas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MATRICULA_NOMBRE,MATRICULA_FECHA,MATRICULA_PERSONAS_ID,MATRICULA_CARRERAS_ID,MATRICULA_CICLOS_ID")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                db.Matriculas.Add(matricula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATRICULA_PERSONAS_ID = new SelectList(db.Personas, "ID", "PERSONA_CODIGO", matricula.MATRICULA_PERSONAS_ID);
            ViewBag.MATRICULA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO", matricula.MATRICULA_CARRERAS_ID);
            ViewBag.MATRICULA_CICLOS_ID = new SelectList(db.Ciclos, "ID", "CICLO_NOMBRE", matricula.MATRICULA_CICLOS_ID);
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATRICULA_PERSONAS_ID = new SelectList(db.Personas, "ID", "PERSONA_CODIGO", matricula.MATRICULA_PERSONAS_ID);
            ViewBag.MATRICULA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO", matricula.MATRICULA_CARRERAS_ID);
            ViewBag.MATRICULA_CICLOS_ID = new SelectList(db.Ciclos, "ID", "CICLO_NOMBRE", matricula.MATRICULA_CICLOS_ID);
            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MATRICULA_NOMBRE,MATRICULA_FECHA,MATRICULA_PERSONAS_ID,MATRICULA_CARRERAS_ID,MATRICULA_CICLOS_ID")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matricula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATRICULA_PERSONAS_ID = new SelectList(db.Personas, "ID", "PERSONA_CODIGO", matricula.MATRICULA_PERSONAS_ID);
            ViewBag.MATRICULA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO", matricula.MATRICULA_CARRERAS_ID);
            ViewBag.MATRICULA_CICLOS_ID = new SelectList(db.Ciclos, "ID", "CICLO_NOMBRE", matricula.MATRICULA_CICLOS_ID);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matricula matricula = db.Matriculas.Find(id);
            db.Matriculas.Remove(matricula);
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
