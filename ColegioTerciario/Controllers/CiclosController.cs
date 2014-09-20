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
    public class CiclosController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Ciclos
        public ActionResult Index()
        {
            return View(db.Ciclos.ToList());
        }

        // GET: Ciclos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciclo ciclo = db.Ciclos.Find(id);
            if (ciclo == null)
            {
                return HttpNotFound();
            }
            return View(ciclo);
        }

        // GET: Ciclos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ciclos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CICLO_NOMBRE,CICLO_INICIO,CICLO_FIN,CICLO_ANIO,CICLO_MATRICULA_INICIO,CICLO_MATRICULA_FIN")] Ciclo ciclo)
        {
            if (ModelState.IsValid)
            {
                db.Ciclos.Add(ciclo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ciclo);
        }

        // GET: Ciclos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciclo ciclo = db.Ciclos.Find(id);
            if (ciclo == null)
            {
                return HttpNotFound();
            }
            return View(ciclo);
        }

        // POST: Ciclos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CICLO_NOMBRE,CICLO_INICIO,CICLO_FIN,CICLO_ANIO,CICLO_MATRICULA_INICIO,CICLO_MATRICULA_FIN")] Ciclo ciclo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciclo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ciclo);
        }

        // GET: Ciclos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciclo ciclo = db.Ciclos.Find(id);
            if (ciclo == null)
            {
                return HttpNotFound();
            }
            return View(ciclo);
        }

        // POST: Ciclos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ciclo ciclo = db.Ciclos.Find(id);
            db.Ciclos.Remove(ciclo);
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
