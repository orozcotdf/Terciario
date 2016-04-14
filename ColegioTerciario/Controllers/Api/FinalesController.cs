using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ColegioTerciario.DAL.Models;
using ColegioTerciario.Lib;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;
using System.Globalization;

namespace ColegioTerciario.Controllers.Api
{
    public class FinalesController : ApiController
    {
        private readonly ColegioTerciarioContext _db = new ColegioTerciarioContext();

        public AjaxCollectionResponseViewModel GetFinales([FromUri]AjaxCollectionParamViewModel param)
        {
            int cantidadRegistros = 0;
            IQueryable<Acta_Examen> actas = _db.Actas_Examenes
                .OrderByDescending(e => e.ACTA_EXAMEN_FECHA);

            if (param.Filtro != null)
            {
                DateTime fecha = new DateTime();
                // DateTime fechaBusqueda = DateTime.ParseExact(param.sSearch, "dd/MM/yyyy", CultureInfo.InvariantCulture);//

                bool esFecha = DateTime.TryParseExact(param.Filtro, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out fecha);

                if (esFecha)
                {
                    actas = actas.Where(c =>
                        c.ACTA_EXAMEN_FECHA.Value.Day == fecha.Day &&
                        c.ACTA_EXAMEN_FECHA.Value.Month == fecha.Month &&
                        c.ACTA_EXAMEN_FECHA.Value.Year == fecha.Year);
                    cantidadRegistros = actas.Count();
                }
                else {

                    try
                    {
                        int id = int.Parse(param.Filtro);
                        actas = actas.Where(c =>
                            c.ID.Equals(id) ||
                                //c.ACTA_EXAMEN_FECHA == fecha  ||
                            c.ACTA_EXAMEN_CARRERA.CARRERA_NOMBRE.ToLower().Contains(param.Filtro.ToLower()) ||
                            c.ACTA_EXAMEN_MATERIA.MATERIA_NOMBRE.ToLower().Contains(param.Filtro.ToLower()) ||
                            c.ACTA_EXAMEN_FOLIO.ToLower().Contains(param.Filtro.ToLower()) ||
                            c.ACTA_EXAMEN_LIBRO.ToLower().Contains(param.Filtro.ToLower())
                        );
                    }
                    catch (Exception)
                    {
                        actas = actas.Where(c =>
                                //c.ACTA_EXAMEN_FECHA == fecha  ||
                            c.ACTA_EXAMEN_CARRERA.CARRERA_NOMBRE.ToLower().Contains(param.Filtro.ToLower()) ||
                            c.ACTA_EXAMEN_MATERIA.MATERIA_NOMBRE.ToLower().Contains(param.Filtro.ToLower()) ||
                            c.ACTA_EXAMEN_FOLIO.ToLower().Contains(param.Filtro.ToLower()) ||
                            c.ACTA_EXAMEN_LIBRO.ToLower().Contains(param.Filtro.ToLower())
                        );
                    }

                    cantidadRegistros = actas.Count();
                    
                }
            }
            else
            {
                cantidadRegistros = actas.Count();
            }


            IQueryable<object> vm = actas
                .Skip(param.Pagina * param.RegistrosPorPagina)
                .Take(param.RegistrosPorPagina)
                .Select(a => new
                {
                    ACTA_EXAMEN_FECHA = a.ACTA_EXAMEN_FECHA.ToString(),
                    ACTA_EXAMEN_LIBRO = a.ACTA_EXAMEN_LIBRO,
                    ACTA_EXAMEN_FOLIO = a.ACTA_EXAMEN_FOLIO,
                    ACTA_EXAMEN_TURNO_EXAMEN = a.ACTA_EXAMEN_TURNO_EXAMEN.TURNO_EXAMEN_NOMBRE,
                    ACTA_EXAMEN_CARRERA = a.ACTA_EXAMEN_CARRERA.CARRERA_NOMBRE_CORTO,
                    ACTA_EXAMEN_MATERIA = a.ACTA_EXAMEN_MATERIA.MATERIA_NOMBRE,
                    ACTA_EXAMEN_NUMERO = a.ID,
                    ID = a.ID
                });


            AjaxCollectionResponseViewModel rvm = new AjaxCollectionResponseViewModel
            {
                Resultados = vm,
                CantidadResultados = cantidadRegistros
            };

            return rvm;
        }
    }
}
