using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;
using PagedList;

namespace ColegioTerciario.Controllers
{
    public class MateriasController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Materias
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            #region Preparando variables para ordenado
            ViewBag.NombreSort = sortOrder == "nombre" ? "nombre_desc" : "nombre";
            ViewBag.NombreCortoSort = sortOrder == "nombre_corto" ? "nombre_corto_desc" : "nombre_corto";
            ViewBag.AnioSort = sortOrder == "anio" ? "anio_desc" : "anio";
            ViewBag.DuracionSort = sortOrder == "duracion" ? "duracion_desc" : "duracion";
            ViewBag.HorasCatedraSort = sortOrder == "horas_catedra" ? "horas_catedra_desc" : "horas_catedra";
            #endregion

            IQueryable<Materia> materias = from m in db.Materias select m;

            #region Busqueda
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                materias = materias.Where(m => m.MATERIA_NOMBRE.ToUpper().Contains(searchString.ToUpper())
                                            || m.MATERIA_NOMBRE.ToUpper().Contains(searchString.ToUpper())
                                       );
            }
            #endregion  

            #region Ordenando en funcion de las variables
            switch (sortOrder)
            {
                case "nombre":
                    materias = materias.OrderBy(m => m.MATERIA_NOMBRE);
                    break;
                case "nombre_desc":
                    materias = materias.OrderByDescending(m => m.MATERIA_NOMBRE);
                    break;
                case "nombre_corto":
                    materias = materias.OrderBy(m => m.MATERIA_NOMBRE_CORTO);
                    break;
                case "nombre_corto_desc":
                    materias = materias.OrderByDescending(m => m.MATERIA_NOMBRE_CORTO);
                    break;
                case "anio":
                    materias = materias.OrderBy(m => m.MATERIA_ANIO);
                    break;
                case "anio_desc":
                    materias = materias.OrderByDescending(m => m.MATERIA_ANIO);
                    break;
                case "duracion":
                    materias = materias.OrderBy(m => m.MATERIA_DURACION);
                    break;
                case "duracion_desc":
                    materias = materias.OrderByDescending(m => m.MATERIA_DURACION);
                    break;
                case "horas_catedra":
                    materias = materias.OrderBy(m => m.MATERIA_HORAS_CATEDRA);
                    break;
                case "horas_catedra_desc":
                    materias = materias.OrderByDescending(m => m.MATERIA_HORAS_CATEDRA);
                    break;
                default:
                    materias = materias.OrderBy(m => m.MATERIA_NOMBRE);
                    break;
            }
            #endregion

            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(materias.ToPagedList(pageNumber, pageSize));
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
            ViewBag.MATERIA_CARRERAS_ID = new SelectList(db.Carreras, "ID", "CARRERA_NOMBRE");
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
