using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.Models;
using ColegioTerciario.DAL.Models;
using System.Web.Script.Serialization;
using ColegioTerciario.Models.Repositories;
using PagedList;
using Newtonsoft.Json;

namespace ColegioTerciario.Controllers
{
    public class CursosController : Controller
    {
        ColegioTerciarioContext db = new ColegioTerciarioContext();
        // GET: Cursos
        public ActionResult Index(JQueryDataTableParamModel param)
        {
            if (Request.IsAjaxRequest())
            {
                var cursosIDS = db.Materias_X_Cursos.Select(c => c.MATERIA_X_CURSO_CURSO_NOMBRE).Distinct().ToList();
                var cursos = db.Materias_X_Cursos
                    .Include("MATERIA_X_CURSO_CARRERA")
                    .Include("MATERIA_X_CURSO_CICLO")
                    .Include("MATERIA_X_CURSO_SEDE")
                    .Select(c => new {
                          c.MATERIA_X_CURSO_CARRERA,
                          c.MATERIA_X_CURSO_CICLO,
                          c.MATERIA_X_CURSO_CURSO_NOMBRE,
                          c.MATERIA_X_CURSO_SEDE
                     })
                    .Distinct()
                    .ToList();
                
                             
                var cursosFiltrados = (from c in cursos
                                         where (param.sSearch == null ||
                                         c.MATERIA_X_CURSO_CARRERA.CARRERA_NOMBRE.ToLower().Contains(param.sSearch.ToLower()) ||
                                         c.MATERIA_X_CURSO_CICLO.CICLO_NOMBRE.ToLower().Contains(param.sSearch.ToLower()) ||
                                         c.MATERIA_X_CURSO_CURSO_NOMBRE.ToLower().Contains(param.sSearch.ToLower())
                                         )
                                         select c).ToList();

                var result = from c in cursosFiltrados.Skip(param.iDisplayStart)
                             .Take(param.iDisplayLength)
                             select new[]  {
                             c.MATERIA_X_CURSO_CICLO.CICLO_ANIO,
                             c.MATERIA_X_CURSO_SEDE != null ? c.MATERIA_X_CURSO_SEDE.SEDE_NOMBRE : null,
                             c.MATERIA_X_CURSO_CARRERA != null ? c.MATERIA_X_CURSO_CARRERA.CARRERA_NOMBRE : null,                       
                             c.MATERIA_X_CURSO_CURSO_NOMBRE
                         };

                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = cursos.Count,
                    iTotalDisplayRecords = cursosFiltrados.Count,
                    iDisplayStart = param.iDisplayStart,
                    iDisplayLength = param.iDisplayLength,
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);
            }
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
            ViewBag.error = Session["error"];
            return View();
        }

        // POST: Cursos/agregarAlumnos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregarAlumnos(int MATERIA_X_CURSO_ID, int[] alumnos)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (int personaId in alumnos)
                    {
                        var cursadasConEstaPersona = db.Cursadas.Where(c => c.CURSADA_ALUMNOS_ID == personaId && c.CURSADA_MATERIAS_X_CURSOS_ID == MATERIA_X_CURSO_ID).Count();
                        if (cursadasConEstaPersona == 0)
                        { 
                            Cursada nuevaCursada = new Cursada()
                            {
                                CURSADA_ALUMNOS_ID = personaId,
                                CURSADA_MATERIAS_X_CURSOS_ID = MATERIA_X_CURSO_ID
                            };
                            db.Cursadas.Add(nuevaCursada);
                            db.SaveChanges();
                        }
                        
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    return RedirectToRoute(new System.Web.Routing.RouteValueDictionary() { 
                        {"Controller", "Cursos"}, 
                        {"Action", "Edit"},
                        {"id", MATERIA_X_CURSO_ID}
                    });
                }
                
            }
            return RedirectToRoute(new System.Web.Routing.RouteValueDictionary() { 
                {"Controller", "Cursos"}, 
                {"Action", "Edit"},
                {"id", MATERIA_X_CURSO_ID}
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregarAlumnosACurso(string ciclo, string nombre, int[] alumnos)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try { 
                var cursos = db.Materias_X_Cursos.Include("MATERIA_X_CURSO_MATERIA").Where(
                   c => c.MATERIA_X_CURSO_CICLO.CICLO_ANIO == ciclo &&
                        c.MATERIA_X_CURSO_CURSO_NOMBRE == nombre
                   ).ToList();

                foreach(Materia_x_Curso curso in cursos)
                {
                    foreach(int alumnoId in alumnos)
                    {
                        var cursadasConEstaPersona = db.Cursadas.Where(c => c.CURSADA_ALUMNOS_ID == alumnoId && c.CURSADA_MATERIAS_X_CURSOS_ID == curso.ID).Count();
                        if (cursadasConEstaPersona == 0)
                        {
                            Cursada nuevaCursada = new Cursada()
                            {
                                CURSADA_ALUMNOS_ID = alumnoId,
                                CURSADA_MATERIAS_X_CURSOS_ID = curso.ID
                            };
                            db.Cursadas.Add(nuevaCursada);
                            db.SaveChanges();
                        }
                    }
                }
                transaction.Commit();
                return RedirectToRoute(new System.Web.Routing.RouteValueDictionary() { 
                        {"Controller", "Cursos"}, 
                        {"Action", "editarCurso"},
                        {"ciclo", ciclo},
                        {"nombre", nombre}
                    });
                }
                catch (Exception)
                {
                    return RedirectToRoute(new System.Web.Routing.RouteValueDictionary() { 
                        {"Controller", "Cursos"}, 
                        {"Action", "editarCurso"},
                        {"ciclo", ciclo},
                        {"nombre", nombre}
                    });
                }

            }
            /*
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (int personaId in alumnos)
                    {
                        var cursadasConEstaPersona = db.Cursadas.Where(c => c.CURSADA_ALUMNOS_ID == personaId && c.CURSADA_MATERIAS_X_CURSOS_ID == MATERIA_X_CURSO_ID).Count();
                        if (cursadasConEstaPersona == 0)
                        {
                            Cursada nuevaCursada = new Cursada()
                            {
                                CURSADA_ALUMNOS_ID = personaId,
                                CURSADA_MATERIAS_X_CURSOS_ID = MATERIA_X_CURSO_ID
                            };
                            db.Cursadas.Add(nuevaCursada);
                            db.SaveChanges();
                        }

                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    return RedirectToRoute(new System.Web.Routing.RouteValueDictionary() { 
                        {"Controller", "Cursos"}, 
                        {"Action", "Edit"},
                        {"id", MATERIA_X_CURSO_ID}
                    });
                }

            }
            return RedirectToRoute(new System.Web.Routing.RouteValueDictionary() { 
                {"Controller", "Cursos"}, 
                {"Action", "Edit"},
                {"id", MATERIA_X_CURSO_ID}
            });*/
            return View();
        }

        [HttpPost]
        public JsonResult ponerNota(int pk, string value, string name)
        {
            Cursada cursada = db.Cursadas.Find(pk);
            string nota = value;
            switch (name)
            {
                case("P1"):
                    cursada.CURSADA_NOTA_P1 = nota;
                    break;
                case "R1":
                    cursada.CURSADA_NOTA_R1 = nota;
                    break;
                case "P2":
                    cursada.CURSADA_NOTA_P2 = nota;
                    break;
                case "R2":
                    cursada.CURSADA_NOTA_R2 = nota;
                    break;
                default:
                    break;
            }
            db.SaveChanges();

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }


        // POST: Cursos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int SEDE, int CICLO, int CARRERA, int? AÑO, string[] NROS)
        {
            if (AÑO == null || NROS.All(n => n == "false"))
            {
                Session["error"] = "Debe definir todos los campos";
                return RedirectToAction("create");
            }
            List<Materia_x_Curso> materias_x_cursos = new List<Materia_x_Curso>();
            MateriasXCursoRepository repo = new MateriasXCursoRepository(db);
            try
            {
                Sede sede = db.Sedes.Find(SEDE);
                Ciclo ciclo = db.Ciclos.Find(CICLO);
                Carrera carrera = db.Carreras.Find(CARRERA);
                ICollection<Materia> materias = (from m in carrera.MATERIAS
                                                where m.MATERIA_ANIO == AÑO.ToString()
                                                select m).ToList();
                
                
                foreach (Materia materia in materias)
                {
                    NROS = NROS.Where(nro => nro != "false").ToArray();
                    foreach (string NRO in NROS)
                    {
                        materias_x_cursos.Add(new Materia_x_Curso()
                        {
                            MATERIA_X_CURSO_CARRERA = carrera,
                            MATERIA_X_CURSO_CICLO = ciclo,
                            MATERIA_X_CURSO_CURSO_NOMBRE = carrera.CARRERA_CODIGO + "-" + AÑO + "" + NRO,
                            MATERIA_X_CURSO_MATERIA = materia,
                            MATERIA_X_CURSO_SEDE = sede,
                        });
                    }
                }

                try
                {
                    Session["materias_x_cursos_ids"] = repo.InsertMateriasXCursos(materias_x_cursos);
                }
                catch (Exception)
                {
                    Session["error"] = "El Curso ya existe con esta definición";
                    return RedirectToAction("create");
                }
                

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
                ICollection<Materia_x_Curso> materias_x_cursos = db.Materias_X_Cursos.
                    Include("MATERIA_X_CURSO_CICLO").
                    Include("MATERIA_X_CURSO_CARRERA").
                    Include("MATERIA_X_CURSO_MATERIA").
                    Where(c => ids.Contains(c.ID)).ToList(); //(from m in db.Materias_X_Cursos where ids.Contains(m.ID) select m) .ToList();
                return View(materias_x_cursos);
            }
            return Redirect("/");
            
        }

        [HttpGet]
        public ActionResult editarCurso(string ciclo, string nombre)
        {
            
            var cursos = db.Materias_X_Cursos.Include("MATERIA_X_CURSO_MATERIA").Where(
                c => c.MATERIA_X_CURSO_CICLO.CICLO_ANIO == ciclo &&
                     c.MATERIA_X_CURSO_CURSO_NOMBRE == nombre
                ).ToList();
            ViewBag.CICLO = ciclo;
            ViewBag.NOMBRE = nombre;
            return View(cursos);
        }
        // GET: Cursos/Edit/5
        public ActionResult Edit(int id)
        {
            var curso = db.Materias_X_Cursos
                .Include("MATERIA_X_CURSO_CARRERA")
                .Include("MATERIA_X_CURSO_MATERIA")
                .Include("MATERIA_X_CURSO_CICLO").SingleOrDefault(c => c.ID == id);
            ViewBag.alumnos = db.Cursadas
                .Include("CURSADA_ALUMNO").Where(c => c.CURSADA_MATERIAS_X_CURSOS_ID == id).ToList();
                          
            return View(curso);
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

        [HttpPost]
        public JsonResult SetearFecha(int pk, DateTime? value, string name)
        {
            try
            {
                Materia_x_Curso curso = db.Materias_X_Cursos.Find(pk);
                switch (name)
                {
                    case "P1_FECHA":
                        curso.MATERIA_X_CURSO_P1_FECHA = value;
                        
                        break;
                    case "R1_FECHA":
                        curso.MATERIA_X_CURSO_R1_FECHA = value;
                        break;
                    case "P2_FECHA":
                        curso.MATERIA_X_CURSO_P2_FECHA = value;
                        break;
                    case "R2_FECHA":
                        curso.MATERIA_X_CURSO_R2_FECHA = value;
                        break;
                    default:
                        break;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string json = JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    {"error", ex.Message}
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
           
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}
