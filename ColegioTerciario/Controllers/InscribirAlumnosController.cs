using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;
using ColegioTerciario.DAL.Models;
using MvcFlash.Core;
using MvcFlash.Core.Extensions;
using Rotativa.MVC;

namespace ColegioTerciario.Controllers
{
    [Authorize(Roles = "Admin, Bedel")]
    public class InscribirAlumnosController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Inscripciones
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string dni)
        {
            Persona alumno;
            // TRAER MATERIAS APROBADAS
            // TRAER FECHAS DE EXAMEN DE MI CARRERA

            var actas_examenes = new List<Acta_Examen>();
            try
            {
                alumno =
                    db.Personas.Include("PERSONA_CURSADAS.CURSADA_MATERIA_X_CURSO.MATERIA_X_CURSO_CARRERA")
                        .SingleOrDefault(p => p.PERSONA_DOCUMENTO_NUMERO == dni);

            }
            catch (InvalidOperationException e)
            {
                Flash.Instance.Error("El alumno esta duplicado, avise a los administradores");
                return View();
            }

            var carreras = alumno.PERSONA_CURSADAS.Select(
                    c => c.CURSADA_MATERIA_X_CURSO.MATERIA_X_CURSO_CARRERA
                ).Distinct().ToList();
            var carreras_ids = carreras.Select(c => c.ID).ToArray();

            var turnos = db.Turnos_Examenes
                .Include("ACTAS_EXAMENES.ACTAS_EXAMENES_DETALLES")
                .Where(
                    t =>
                        t.TURNO_EXAMEN_CICLO.CICLO_ANIO == DateTime.Now.Year.ToString() &&
                        t.TURNO_EXAMEN_ESTADO == "Habilitado")
                .Select(
                    t => new InscripcionesViewModel
                    {
                        ID = t.ID,
                        TURNO_EXAMEN_NOMBRE = t.TURNO_EXAMEN_NOMBRE,
                        ACTAS_EXAMENES = t.ACTAS_EXAMENES.Where(
                            a => carreras_ids.Contains(a.ACTA_EXAMEN_CARRERAS_ID.Value) && !a.ACTAS_EXAMENES_DETALLES.Any(detalle => detalle.ACTA_EXAMEN_DETALLE_ALUMNOS_ID == alumno.ID)
                        ).ToList()
                    }
                );


            ViewBag.TURNOS = turnos.ToList();
            // ViewBag.ALUMNO = alumno.ID;
            return View("BuscaAlumnos", alumno);
        }

        [HttpPost]
        public ActionResult GuardarInscripciones(int? idAlumno, int[] mesas, bool chequearCorrelativas = false)
        {
            bool error = false;
            // TRAER CURSADAS
            var resultado = new List<ErrorViewModel>();
            //List<int> inscripciones = new List<int>();
            List<Acta_Examen_Detalle> inscripciones = new List<Acta_Examen_Detalle>();

            var mesasDeExamen =
                db.Actas_Examenes
                    .Where(a => mesas.Contains(a.ID))
                //.Where(a => a.ACTA_EXAMEN_MATERIA.MATERIA_APROBADAS_PARA_RENDIR != null)
                    .Select(a => new
                    {
                        MESA = a.ID,
                        CODIGOS = a.ACTA_EXAMEN_MATERIA.MATERIA_APROBADAS_PARA_RENDIR.Substring(0, a.ACTA_EXAMEN_MATERIA.MATERIA_APROBADAS_PARA_RENDIR.Length - 1)
                    }).ToArray();

            #region correlativas
            if (chequearCorrelativas == true)
            {
                foreach (var mesa in mesasDeExamen)
                {
                    if (mesa.CODIGOS != null)
                    {
                        foreach (var codigo in mesa.CODIGOS.Split(','))
                        {
                            var cursadas = db.Cursadas
                                .Where(c => c.CURSADA_ALUMNOS_ID == idAlumno).ToList();

                            var pepe = db.Cursadas.Include("CURSADA_MATERIA_X_CURSO")
                                .Where(c => c.CURSADA_ALUMNOS_ID == idAlumno && c.CURSADA_MATERIA_X_CURSO.MATERIA_X_CURSO_MATERIA.MATERIA_CODIGO == codigo).ToList();

                            var parciales = db.Cursadas.Include("CURSADA_MATERIA_X_CURSO")
                                .SingleOrDefault(c => c.CURSADA_ALUMNOS_ID == idAlumno && c.CURSADA_MATERIA_X_CURSO.MATERIA_X_CURSO_MATERIA.MATERIA_CODIGO == codigo);


                            if (parciales == null)
                            {
                                // NUNCA RINDIO PARCIAL
                                Flash.Instance.Success("Inscripcion", mesa.MESA + " Incompatible, No Rindio " + codigo);
                                resultado.Add(new ErrorViewModel
                                {
                                    MESA = mesa.MESA,
                                    DESCRIPCION = "No rindio"
                                });
                                error = true;
                            }
                            else if (parciales.CURSADA_ESTADO_ACADEMICO != "Regular")
                            {
                                Flash.Instance.Success("Inscripcion", mesa.MESA + " Incompatible, Mesa Reprobada");
                                resultado.Add(new ErrorViewModel
                                {
                                    MESA = mesa.MESA,
                                    DESCRIPCION = "Mesa reprobada"
                                });
                                error = true;
                            }

                        }

                        
                    }
                }

                if (resultado.Count() > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            #endregion

            foreach (var mesa in mesasDeExamen)
            {

                var acta_examen = db.Actas_Examenes.Find(mesa.MESA);

                Acta_Examen_Detalle acta_examen_detalle = new Acta_Examen_Detalle
                {
                    ACTA_EXAMEN_DETALLE_ALUMNOS_ID = idAlumno,
                };

                acta_examen.ACTAS_EXAMENES_DETALLES.Add(acta_examen_detalle);
                db.SaveChanges();
                inscripciones.Add(acta_examen_detalle);
                
            }

            ViewBag.INSCRIPCIONES = inscripciones;
            ViewBag.PERSONA_NOMBRE_COMPLETO = db.Personas.Single(a => a.ID == idAlumno).PERSONA_NOMBRE_COMPLETO;


            return new ViewAsPdf("ConstanciaDeInscripcion");
        }

        [HttpPost]
        public ActionResult ConstanciaDeInscripcion(int[] inscripciones)
        {
            return new ViewAsPdf();
        }
    }
}