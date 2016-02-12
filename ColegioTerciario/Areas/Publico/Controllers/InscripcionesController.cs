using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.Models;
using Rotativa.Core;
using Rotativa.MVC;

namespace ColegioTerciario.Areas.Publico.Controllers
{
    public class InscripcionesController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        // GET: Publico/Inscripciones
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ImprimirInscripcion(string id)
        {
            //return new Rotativa.ActionAsPdf("GetPersons");
            return new Rotativa.MVC.ActionAsPdf("VerInscripcion", new { id = id })
            {
                RotativaOptions =
                {
                    PageMargins = { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                    PageSize = Rotativa.Core.Options.Size.A4,
                    IsGrayScale = true,
                    IsLowQuality = false
                }

            };
            
        }

        public ActionResult VerInscripcion(string id)
        {
            var inscripcion = db.Inscripciones.Find(Guid.Parse(id));
            var cantidadInscriptosConfirmados = db.Inscripciones.Count(i => i.INSCRIPCIONES_CARRERA_ID == inscripcion.INSCRIPCIONES_CARRERA_ID && i.INSCRIPCIONES_PRESENTO_DOCUMENTACION);
            ViewBag.LISTA_DE_ESPERA = cantidadInscriptosConfirmados >= 100 ? true : false;

            if (inscripcion != null)
            {
                return View(inscripcion);
            }
            else
            {
                return Redirect("/Publico/Inscripciones");
            }
        }
    }
}