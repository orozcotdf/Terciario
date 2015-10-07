﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;
using ColegioTerciario.Models.ViewModels.Api;
using Newtonsoft.Json;
using OrderByExtensions;
namespace ColegioTerciario.Controllers.Api
{
    public class FechaViewModel
    {
        public DateTime? value { get; set; }
        public int pk { get; set; }
        public string name { get; set; }
    }
    public class CursosController : ApiController
    {
        private readonly ColegioTerciarioContext _db;

        public CursosController()
        {
            _db = new ColegioTerciarioContext();
        }

        // GET: api/Cursos

        [HttpGet]
        public object GetCursos([FromUri]DataTableParamModel param)
        {
            var cursos = _db.Materias_X_Cursos
                   .Include("MATERIA_X_CURSO_CARRERA")
                   .Include("MATERIA_X_CURSO_CICLO")
                   .Include("MATERIA_X_CURSO_SEDE")
                   .Select(c => new
                   {
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
                             c.MATERIA_X_CURSO_SEDE != null ? c.MATERIA_X_CURSO_SEDE.ID.ToString() : null,
                             c.MATERIA_X_CURSO_CARRERA != null ? c.MATERIA_X_CURSO_CARRERA.CARRERA_NOMBRE : null,                       
                             c.MATERIA_X_CURSO_CURSO_NOMBRE,
                             c.MATERIA_X_CURSO_SEDE.SEDE_NOMBRE
                         };

            return new
            {
                sEcho = param.sEcho,
                iTotalRecords = cursos.Count,
                iTotalDisplayRecords = cursosFiltrados.Count,
                iDisplayStart = param.iDisplayStart,
                iDisplayLength = param.iDisplayLength,
                aaData = result
            };
        }

        [HttpGet]
        public AjaxCollectionResponseViewModel ObtenerCursos([FromUri]AjaxCollectionParamViewModel param, [FromUri] int? docenteId)
        {

            var cursos = _db.Materias_X_Cursos
                .Where(m => m.MATERIA_X_CURSO_DOCENTE_ID == docenteId)
                .OrderByDescending(m => m.ID)
                .Skip(param.Pagina*param.RegistrosPorPagina)
                .Take(param.RegistrosPorPagina);
            /*
            
            */
            if (param.OrdenarPorColumna != null)
            {
                switch (param.OrdenarPorColumna)
                {
                    case "CICLO_ANIO":
                        cursos = param.OrdenarAsc ? cursos.OrderBy(c => c.MATERIA_X_CURSO_CICLO.CICLO_ANIO) : cursos.OrderByDescending(c => c.MATERIA_X_CURSO_CICLO.CICLO_ANIO);
                        break;
                    case "CARRERA_NOMBRE":
                        cursos = param.OrdenarAsc ? cursos.OrderBy(c => c.MATERIA_X_CURSO_CARRERA.CARRERA_NOMBRE) : cursos.OrderByDescending(c => c.MATERIA_X_CURSO_CARRERA.CARRERA_NOMBRE);
                        break;
                    case "MATERIA_X_CURSO_CURSO_NOMBRE":
                        cursos = param.OrdenarAsc ? cursos.OrderBy(c => c.MATERIA_X_CURSO_CURSO_NOMBRE) : cursos.OrderByDescending(c => c.MATERIA_X_CURSO_CURSO_NOMBRE);
                        break;
                    case "SEDE_NOMBRE":
                        cursos = param.OrdenarAsc ? cursos.OrderBy(c => c.MATERIA_X_CURSO_SEDE.SEDE_NOMBRE) : cursos.OrderByDescending(c => c.MATERIA_X_CURSO_SEDE.SEDE_NOMBRE);
                        break;
                    case "MATERIA_NOMBRE":
                        cursos = param.OrdenarAsc ? cursos.OrderBy(c => c.MATERIA_X_CURSO_MATERIA.MATERIA_NOMBRE) : cursos.OrderByDescending(c => c.MATERIA_X_CURSO_MATERIA.MATERIA_NOMBRE);
                        break;
                }
            }
            else
            {
                cursos = param.OrdenarAsc ? cursos.OrderBy(c => c.ID) : cursos.OrderByDescending(c => c.ID);
            }
            return new AjaxCollectionResponseViewModel
            {
                CantidadResultados = _db.Materias_X_Cursos.Count(m => m.MATERIA_X_CURSO_DOCENTE_ID == docenteId),
                Resultados = cursos.Select(c =>  new CursosPorDocenteViewModel {
                            ID = c.ID,
                            CICLO_ANIO = c.MATERIA_X_CURSO_CICLO.CICLO_ANIO,
                            MATERIA_X_CURSO_SEDES_ID = c.MATERIA_X_CURSO_SEDES_ID,
                            CARRERA_NOMBRE = c.MATERIA_X_CURSO_CARRERA != null ? c.MATERIA_X_CURSO_CARRERA.CARRERA_NOMBRE : null,                       
                            MATERIA_X_CURSO_CURSO_NOMBRE = c.MATERIA_X_CURSO_CURSO_NOMBRE,
                            SEDE_NOMBRE = c.MATERIA_X_CURSO_SEDE.SEDE_NOMBRE,
                            MATERIA_NOMBRE = c.MATERIA_X_CURSO_MATERIA.MATERIA_NOMBRE
                        })
            };
        }

        [HttpPost]
        public object PonerNota([FromBody]int pk, [FromBody]string value, [FromBody]string name)
        {

            Cursada cursada = _db.Cursadas.Find(pk);
            string nota = value;
            switch (name)
            {
                case ("P1"):
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
            }

            _db.SaveChanges();

            return new {};
        }

        [HttpPost]
        public IHttpActionResult SetearFecha([FromBody]FechaViewModel param)
        {
            try
            {
                Materia_x_Curso curso = _db.Materias_X_Cursos.Find(param.pk);
                switch (param.name)
                {
                    case "P1_FECHA":
                        curso.MATERIA_X_CURSO_P1_FECHA = param.value;

                        break;
                    case "R1_FECHA":
                        curso.MATERIA_X_CURSO_R1_FECHA = param.value;
                        break;
                    case "P2_FECHA":
                        curso.MATERIA_X_CURSO_P2_FECHA = param.value;
                        break;
                    case "R2_FECHA":
                        curso.MATERIA_X_CURSO_R2_FECHA = param.value;
                        break;
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                /*string json = JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    {"error", ex.Message}
                });
                return Json(json, JsonRequestBehavior.AllowGet);*/
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ConfigurarHorarios(int id, [FromBody] object Lunes)
        {
            return Ok();
        }

        [HttpPost]
        public object SetearTurno([FromBody]string value, [FromBody]int pk) {
            return new {};
        }

        [HttpPost]
        public IHttpActionResult CerrarNotas(int id)
        {
            try
            {
                Materia_x_Curso mc = _db.Materias_X_Cursos.Find(id);
                mc.MATERIA_X_CURSO_DEFINITIVO_EN_LIBRO = true;
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}