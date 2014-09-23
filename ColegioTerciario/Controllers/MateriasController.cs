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
    public class MateriasController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Materias
        public ActionResult Index()
        {
            var materias = db.Materias.Include(m => m.MATERIA_CARRERA);
            return View(materias.ToList());
        }

        // GET: Materias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        // GET: Materias/Create
        public ActionResult Create()
        {
            ViewBag.MATERIA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO");
            return View();
        }

        // POST: Materias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MATERIA_CODIGO,MATERIA_CARRERAS_ID,MATERIA_ANIO,MATERIA_APROBADAS_PARA_CURSAR,MATERIA_APROBADAS_PARA_RENDIR,MATERIA_CURSADAS_PARA_CURSAR,MATERIA_CURSADAS_PARA_RENDIR,MATERIA_DURACION,MATERIA_HORAS_CATEDRA,MATERIA_NOMBRE,MATERIA_NOMBRE_CORTO")] Materia materia)
        {
            if (ModelState.IsValid)
            {
                db.Materias.Add(materia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATERIA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO", materia.MATERIA_CARRERAS_ID);
            return View(materia);
        }

        // GET: Materias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATERIA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO", materia.MATERIA_CARRERAS_ID);
            return View(materia);
        }

        // POST: Materias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MATERIA_CODIGO,MATERIA_CARRERAS_ID,MATERIA_ANIO,MATERIA_APROBADAS_PARA_CURSAR,MATERIA_APROBADAS_PARA_RENDIR,MATERIA_CURSADAS_PARA_CURSAR,MATERIA_CURSADAS_PARA_RENDIR,MATERIA_DURACION,MATERIA_HORAS_CATEDRA,MATERIA_NOMBRE,MATERIA_NOMBRE_CORTO")] Materia materia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATERIA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_CODIGO", materia.MATERIA_CARRERAS_ID);
            return View(materia);
        }

        // GET: Materias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materia materia = db.Materias.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Materia materia = db.Materias.Find(id);
            db.Materias.Remove(materia);
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
