using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColegioTerciario.Areas.Alumnos.Controllers
{
    public class SesionesController : Controller
    {
        // GET: Alumnos/Sesion
        [AllowAnonymous]
        public ActionResult Entrar(string returnUrl)
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Registrar()
        {
            return View();
        }
    }
}