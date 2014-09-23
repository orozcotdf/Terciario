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
    public class BarriosController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Barrios
        public ActionResult Index()
        {
            var barrios = db.Barrios.Include(b => b.BARRIO_CIUDAD);
            return View(barrios.ToList());
        }

        // GET: Barrios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barrio barrio = db.Barrios.Find(id);
            if (barrio == null)
            {
                return HttpNotFound();
            }
            return View(barrio);
        }

        // GET: Barrios/Create
        public ActionResult Create()
        {
            ViewBag.BARRIO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME");
            return View();
        }

        // POST: Barrios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BARRIO_NOMBRE,BARRIO_CIUDAD_ID")] Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                db.Barrios.Add(barrio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BARRIO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME", barrio.BARRIO_CIUDAD_ID);
            return View(barrio);
        }

        // GET: Barrios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barrio barrio = db.Barrios.Find(id);
            if (barrio == null)
            {
                return HttpNotFound();
            }
            ViewBag.BARRIO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME", barrio.BARRIO_CIUDAD_ID);
            return View(barrio);
        }

        // POST: Barrios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BARRIO_NOMBRE,BARRIO_CIUDAD_ID")] Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barrio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BARRIO_CIUDAD_ID = new SelectList(db.Ciudades, "ID", "CIUDAD_NAME", barrio.BARRIO_CIUDAD_ID);
            return View(barrio);
        }

        // GET: Barrios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barrio barrio = db.Barrios.Find(id);
            if (barrio == null)
            {
                return HttpNotFound();
            }
            return View(barrio);
        }

        // POST: Barrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Barrio barrio = db.Barrios.Find(id);
            db.Barrios.Remove(barrio);
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
