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
using PagedList;


namespace ColegioTerciario.Controllers
{
    public class PersonasController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Personas
        
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.EmailSortParam = sortOrder;
            ViewBag.DomicilioSortParm = sortOrder;
            ViewBag.TelefonoSortParm = sortOrder;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = sortOrder == "nombre" ? "nombre_desc" : "nombre";
            ViewBag.ApellidoSortParm = sortOrder == "apellido" ? "apellido_desc" : "apellido";
            ViewBag.DocSortParm = sortOrder == "documento" ? "documento_desc" : "documento";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var personas = from s in db.Personas
                           select s;
            
            //busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                personas = personas.Where(s => s.PERSONA_NOMBRE.ToUpper().Contains(searchString.ToUpper())
                                            || s.PERSONA_APELLIDO.ToUpper().Contains(searchString.ToUpper())
                                            || s.PERSONA_DOCUMENTO_NUMERO.ToUpper().Contains(searchString.ToUpper())
                                       );
            }

            //Orden
            switch (sortOrder)
            {
                case "nombre":
                    personas = personas.OrderBy(s => s.PERSONA_NOMBRE);
                    break;
                case "nombre_desc":
                    personas = personas.OrderByDescending(s => s.PERSONA_NOMBRE);
                    break;
                case "documento":
                    personas = personas.OrderBy(s => s.PERSONA_DOCUMENTO_NUMERO);
                    break;
                case "documento_desc":
                    personas = personas.OrderByDescending(s => s.PERSONA_DOCUMENTO_NUMERO);
                    break;
                case "apellido_desc":
                    personas = personas.OrderByDescending(s => s.PERSONA_APELLIDO);
                    break;
                default:
                    personas = personas.OrderBy(s => s.PERSONA_APELLIDO); //por defecto orden por apellido ascendente
                    break;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(personas.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult IndexJSON(JQueryDataTableParamModel param)
        {
            var personas = db.Personas;
            List<Persona> personasFiltradas;

            if (param.sSearch == null)
            {
                personasFiltradas = personas.ToList();
            }
            else
            {
                personasFiltradas = (from e in personas
                                    where (
                                    e.PERSONA_DOCUMENTO_NUMERO.ToLower().Contains(param.sSearch.ToLower()) ||
                                    e.PERSONA_NOMBRE.ToLower().Contains(param.sSearch.ToLower()) ||
                                    e.PERSONA_APELLIDO.ToLower().Contains(param.sSearch.ToLower()))
                                    select e).ToList();
            }
            var result = from p in personasFiltradas.Skip(param.iDisplayStart)
                         .Take(param.iDisplayLength)
                         select new[]  {
                             Convert.ToString(p.ID),
                             p.PERSONA_NOMBRE,
                             p.PERSONA_APELLIDO,
                             p.PERSONA_DOCUMENTO_NUMERO
                         };
            
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = personas.Count(),
                iTotalDisplayRecords = personasFiltradas.Count,
                iDisplayStart = param.iDisplayStart,
                iDisplayLength = param.iDisplayLength,
                aaData = result
            },
            JsonRequestBehavior.AllowGet);
            

            /*
             * public ActionResult MasterDetailsAjaxHandler(
             JQueryDataTableParamModel param, int? CompanyID)
    {

        var employees = DataRepository.GetEmployees();

        //"Business logic" method that filters employees by the employer id
        var companyEmployees = (from e in employees
                                where (CompanyID == null || e.CompanyID == CompanyID)
                                select e).ToList();

        //UI processing logic that filter company employees by name and paginates them
        var filteredEmployees = (from e in companyEmployees
                                 where (param.sSearch == null || 
                                 e.Name.ToLower().Contains(param.sSearch.ToLower()))
                                 select e).ToList();
        var result = from emp in filteredEmployees.Skip(
                     param.iDisplayStart).Take(param.iDisplayLength)
                     select new[] { Convert.ToString(emp.EmployeeID), 
                     emp.Name, emp.Position };

        return Json(new
        {
            sEcho = param.sEcho,
            iTotalRecords = companyEmployees.Count,
            iTotalDisplayRecords = filteredEmployees.Count,
            aaData = result
        },
        JsonRequestBehavior.AllowGet);
    }*/
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
            ViewBag.PERSONA_NACIMIENTO_BARRIO_ID = new SelectList(db.Barrios, "ID", "BARRIO_NOMBRE");
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
        public ActionResult Create([Bind(Include = "ID,PERSONA_CODIGO,PERSONA_USUARIO,PERSONA_CLAVE,PERSONA_NOMBRE,PERSONA_APELLIDO,PERSONA_NOMBRE_PARA_MOSTRAR,PERSONA_DOCUMENTO_TIPO,PERSONA_DOCUMENTO_NUMERO,PERSONA_NACIMIENTO_FECHA,PERSONA_EMAIL,PERSONA_DOMICILIO,PERSONA_TELEFONO,PERSONA_SEXO,PERSONA_FECHA_ALTA,PERSONA_FECHA_BAJA,PERSONA_TITULO_SECUNDARIO,PERSONA_OBSERVACION,PERSONA_FOTO,PERSONA_CUIL,PERSONA_ES_ALUMNO,PERSONA_ES_DOCENTE,PERSONA_ES_NODOCENTE,PERSONA_NACIMIENTO_PAIS_ID,PERSONA_NACIMIENTO_PROVINCIA_ID,PERSONA_NACIMIENTO_CIUDAD_ID,PERSONA_NACIMIENTO_BARRIO_ID")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = persona.ID });
            }

            ViewBag.PERSONA_NACIMIENTO_BARRIO_ID = new SelectList(db.Barrios, "ID", "BARRIO_NOMBRE", persona.PERSONA_NACIMIENTO_BARRIO_ID);
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
            ViewBag.PERSONA_NACIMIENTO_BARRIO_ID = new SelectList(db.Barrios, "ID", "BARRIO_NOMBRE", persona.PERSONA_NACIMIENTO_BARRIO_ID);
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
        public ActionResult Edit([Bind(Include = "ID,PERSONA_CODIGO,PERSONA_USUARIO,PERSONA_CLAVE,PERSONA_NOMBRE,PERSONA_APELLIDO,PERSONA_NOMBRE_PARA_MOSTRAR,PERSONA_DOCUMENTO_TIPO,PERSONA_DOCUMENTO_NUMERO,PERSONA_NACIMIENTO_FECHA,PERSONA_EMAIL,PERSONA_DOMICILIO,PERSONA_TELEFONO,PERSONA_SEXO,PERSONA_FECHA_ALTA,PERSONA_FECHA_BAJA,PERSONA_TITULO_SECUNDARIO,PERSONA_OBSERVACION,PERSONA_FOTO,PERSONA_CUIL,PERSONA_ES_ALUMNO,PERSONA_ES_DOCENTE,PERSONA_ES_NODOCENTE,PERSONA_NACIMIENTO_PAIS_ID,PERSONA_NACIMIENTO_PROVINCIA_ID,PERSONA_NACIMIENTO_CIUDAD_ID,PERSONA_NACIMIENTO_BARRIO_ID")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = persona.ID });
            }
            ViewBag.PERSONA_NACIMIENTO_BARRIO_ID = new SelectList(db.Barrios, "ID", "BARRIO_NOMBRE", persona.PERSONA_NACIMIENTO_BARRIO_ID);
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
