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
    public class HorasController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Horas
        public ActionResult Index()
        {
            return View(db.Horas.ToList());
        }

        // GET: Horas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hora hora = db.Horas.Find(id);
            if (hora == null)
            {
                return HttpNotFound();
            }
            return View(hora);
        }

        // GET: Horas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HORA_INICIO,HORA_FIN,HORA_NOMBRE,HORA_TURNO")] Hora hora)
        {
            if (ModelState.IsValid)
            {
                db.Horas.Add(hora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hora);
        }

        // GET: Horas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hora hora = db.Horas.Find(id);
            if (hora == null)
            {
                return HttpNotFound();
            }
            return View(hora);
        }

        // POST: Horas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HORA_INICIO,HORA_FIN,HORA_NOMBRE,HORA_TURNO")] Hora hora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hora);
        }

        // GET: Horas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hora hora = db.Horas.Find(id);
            if (hora == null)
            {
                return HttpNotFound();
            }
            return View(hora);
        }

        // POST: Horas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hora hora = db.Horas.Find(id);
            db.Horas.Remove(hora);
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
