using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;
using PagedList;

namespace ColegioTerciario.Controllers
{
    public class PersonasController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Personas
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
           // return View(await db.Personas.ToListAsync());
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var _personas = from s in db.Personas
                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                _personas = _personas.Where(s => s.PERSONA_APELLIDO.ToUpper().Contains(searchString.ToUpper())
                                       || s.PERSONA_NOMBRE.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    _personas = _personas.OrderByDescending(s => s.PERSONA_NOMBRE);
                break;
                case "Date":
                    _personas = _personas.OrderBy(s => s.PERSONA_NACIMIENTO_FECHA);
                break;
                default:
                    _personas = _personas.OrderBy(s => s.PERSONA_APELLIDO);
                break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_personas.ToPagedList(pageNumber, pageSize));
            //return View(_personas.ToList());

        }

        // GET: Personas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = await db.Personas.FindAsync(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PERSONA_CODIGO,PERSONA_USUARIO,PERSONA_CLAVE,PERSONA_NOMBRE,PERSONA_APELLIDO,PERSONA_DOCUMENTO_TIPO,PERSONA_DOCUMENTO_NUMERO,PERSONA_NACIMIENTO_FECHA,PERSONA_EMAIL,PERSONA_DOMICILIO,PERSONA_TELEFONO,PERSONA_SEXO,PERSONA_FECHA_ALTA,PERSONA_FECHA_BAJA,PERSONA_TITULO_SECUNDARIO,PERSONA_OBSERVACION,PERSONA_FOTO,PERSONA_NACIMIENTO_PAIS_ID,PERSONA_NACIMIENTO_PROVINCIA_ID,PERSONA_NACIMIENTO_LOCALIDAD_ID,PERSONA_CUIL,PERSONA_BARRIO_ID,PERSONA_ES_ALUMNO,PERSONA_ES_DOCENTE,PERSONA_ES_NODOCENTE")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(personas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(personas);
        }

        // GET: Personas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = await db.Personas.FindAsync(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PERSONA_CODIGO,PERSONA_USUARIO,PERSONA_CLAVE,PERSONA_NOMBRE,PERSONA_APELLIDO,PERSONA_DOCUMENTO_TIPO,PERSONA_DOCUMENTO_NUMERO,PERSONA_NACIMIENTO_FECHA,PERSONA_EMAIL,PERSONA_DOMICILIO,PERSONA_TELEFONO,PERSONA_SEXO,PERSONA_FECHA_ALTA,PERSONA_FECHA_BAJA,PERSONA_TITULO_SECUNDARIO,PERSONA_OBSERVACION,PERSONA_FOTO,PERSONA_NACIMIENTO_PAIS_ID,PERSONA_NACIMIENTO_PROVINCIA_ID,PERSONA_NACIMIENTO_LOCALIDAD_ID,PERSONA_CUIL,PERSONA_BARRIO_ID,PERSONA_ES_ALUMNO,PERSONA_ES_DOCENTE,PERSONA_ES_NODOCENTE")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(personas);
        }

        // GET: Personas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = await db.Personas.FindAsync(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Personas personas = await db.Personas.FindAsync(id);
            db.Personas.Remove(personas);
            await db.SaveChangesAsync();
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
