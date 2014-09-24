using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.Models;
using ColegioTerciario.DAL.Models;
using System.Web.Script.Serialization;
using ColegioTerciario.Models.Repositories;

namespace ColegioTerciario.Controllers
{
    public class CursosController : Controller
    {
        ColegioTerciarioContext db = new ColegioTerciarioContext();
        // GET: Cursos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cursos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cursos/Create
        public ActionResult Create()
        {
            ViewBag.CICLOS = new SelectList(db.Ciclos, "ID", "CICLO_NOMBRE");
            ViewBag.CARRERAS = new SelectList(db.Carreras, "ID", "CARRERA_NOMBRE");
            ViewBag.SEDES = new SelectList(db.Sedes, "ID", "SEDE_NOMBRE");
            return View();
        }

        // POST: Cursos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int SEDE, int CICLO, int CARRERA, int AÑO)
        {
            List<Materia_x_Curso> materias_x_cursos = new List<Materia_x_Curso>();
            MateriasXCursoRepository repo = new MateriasXCursoRepository(db);
            try
            {
                Sede sede = db.Sedes.Find(SEDE);
                Ciclo ciclo = db.Ciclos.Find(CICLO);
                Carrera carrera = db.Carreras.Find(CARRERA);
                ICollection<Materia> materias = carrera.MATERIAS;
                
                
                foreach (Materia materia in materias)
                {
                    materias_x_cursos.Add(new Materia_x_Curso()
                    {
                        MATERIA_X_CURSO_CARRERA = carrera,
                        MATERIA_X_CURSO_CICLO = ciclo,
                        MATERIA_X_CURSO_CURSO_NOMBRE = AÑO + "A",
                        MATERIA_X_CURSO_MATERIA = materia,
                        MATERIA_X_CURSO_SEDE = sede,
                    });

                }
                Session["materias_x_cursos_ids"] = repo.InsertMateriasXCursos(materias_x_cursos);

                return RedirectToAction("Resumen");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Resumen()
        {
            if (Session["materias_x_cursos_ids"] != null)
            { 
                ICollection<int> ids = Session["materias_x_cursos_ids"] as ICollection<int>;
                ICollection<Materia_x_Curso> materias_x_cursos = (from m in db.Materias_X_Cursos where ids.Contains(m.ID) select m).ToList();
                return View();
            }
            return Redirect("/");
            
        }
        // GET: Cursos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cursos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cursos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cursos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
