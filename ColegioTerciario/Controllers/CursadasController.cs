using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;

namespace ColegioTerciario.Controllers
{
    public class CursadasController : Controller
    {
        private readonly ColegioTerciarioContext _db = new ColegioTerciarioContext();

        [System.Web.Http.HttpPost]
        public JsonResult Set([FromUri]int pk, [FromUri]string value, [FromUri]string name)
        {
            Cursada cursada = _db.Cursadas.Find(pk);
            if (value != "")
            {
                switch (name)
                {
                    case ("CURSADA_ESTADO_ACADEMICO"):
                        cursada.CURSADA_ESTADO_ACADEMICO = value;
                        break;
                    case ("CURSADA_ESTADO_ASISTENCIA"):
                        cursada.CURSADA_ESTADO_ASISTENCIA = value;
                        break;
                    case ("CURSADA_ESTADO_DEFINITIVO"):
                        cursada.CURSADA_ESTADO_DEFINITIVO = value;
                        break;
                }

            }

            _db.SaveChanges();

            return Json(new { message = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}