using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColegioTerciario.Models;
using ColegioTerciario.Models.ViewModels;

namespace ColegioTerciario.Controllers.Api
{
    public class MateriasController : ApiController
    {
        private readonly ColegioTerciarioContext _db;
        public MateriasController()
        {
            _db = new ColegioTerciarioContext();
            ;
        }

        //[HttpGet]
        public IHttpActionResult GetPorCarrera([FromUri]int carreraId)
        {
            var materias = _db.Materias.Where(m => m.MATERIA_CARRERAS_ID == carreraId)
                .Select(m => new {m.ID, m.MATERIA_NOMBRE});
            return Ok(materias);
        }

        [HttpGet]
        public IQueryable<ReactSelectViewModel> SelectMaterias([FromUri]string busqueda)
        {
            return (from m in _db.Materias
                    where (
                        m.MATERIA_NOMBRE.ToLower().Contains(busqueda.ToLower()) ||
                        m.MATERIA_NOMBRE_CORTO.ToLower().Contains(busqueda.ToLower())
                        )

                    select new ReactSelectViewModel
                    {
                        label = m.MATERIA_NOMBRE,
                        value = m.ID.ToString()
                    }).Take(5);
        }
    }
}
