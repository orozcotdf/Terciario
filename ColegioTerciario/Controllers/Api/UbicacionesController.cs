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
    public class UbicacionesController : ApiController
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        [HttpGet]
        public IQueryable<ReactSelectViewModel> Paises([FromUri]string busqueda)
        {
            return (from c in db.Paises
                    where (
                        c.PAIS_NAME.Contains(busqueda)
                        )

                    select new ReactSelectViewModel
                    {
                        label = c.PAIS_NAME,
                        value = c.ID.ToString()
                    });
        }
        [HttpGet]
        public IQueryable<ReactSelectViewModel> Provincias([FromUri]string busqueda)
        {
            return (from c in db.Provincias
                    where (
                        c.PROVINCIA_NAME.Contains(busqueda)
                        )

                    select new ReactSelectViewModel
                    {
                        label = c.PROVINCIA_NAME,
                        value = c.ID.ToString()
                    });
        }
        [HttpGet]
        public IQueryable<ReactSelectViewModel> Ciudades([FromUri]string busqueda)
        {
            return (from c in db.Ciudades
                    where (
                        c.CIUDAD_NAME.Contains(busqueda)
                        )

                    select new ReactSelectViewModel
                    {
                        label = c.CIUDAD_NAME,
                        value = c.ID.ToString()
                    });
        }

        [HttpGet]
        public IQueryable<ReactSelectViewModel> Barrios([FromUri]string busqueda)
        {
            return (from c in db.Barrios
                    where (
                        c.BARRIO_NOMBRE.Contains(busqueda)
                        )

                    select new ReactSelectViewModel
                    {
                        label = c.BARRIO_NOMBRE,
                        value = c.ID.ToString()
                    }).Take(5);
        }
    }
}
