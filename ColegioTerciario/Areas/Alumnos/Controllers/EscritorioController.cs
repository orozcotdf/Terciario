using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColegioTerciario.DAL.Models;
using ColegioTerciario.Migrations;
using ColegioTerciario.Models;
using ColegioTerciario.Models.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ColegioTerciario.Areas.Alumnos
{
    public class EscritorioController : Controller
    {
        private ColegioTerciarioContext db = new ColegioTerciarioContext();

        [Authorize(Roles = "Alumno")]
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            Persona persona = db.Personas.SingleOrDefault(a => a.ID == currentUser.USER_PERSONA_ID);

            var _repo = new PersonasRepository();
            ViewBag.Message = "Your application description page.";
            ViewBag.SITUACIONPORCICLOS = _repo.GetSituacionAcademicaPorCiclos(persona).ToList();
            ViewBag.SITUACIONPORMATERIAS = _repo.GetSituacionAcademicaPorMaterias(persona).ToList();
            ViewBag.FINALES = _repo.GetFinales(persona).ToList();
            return View();
        }
    }
}