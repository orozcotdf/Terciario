using ColegioTerciario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using ColegioTerciario.Areas.Admin.Models;
using ColegioTerciario.Models.Repositories;

namespace ColegioTerciario.Areas.Admin.Controllers
{
    public class UsuariosController : AdminController
    {
        private ColegioTerciarioContext context;
        public UsuariosController()
        {
            context = GetContext();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin/Seguridad
        public ActionResult Index()
        {
            List<UserViewModel> usuarios = new List<UserViewModel>();

            var users = UserManager.Users.ToList();

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);



            foreach (ApplicationUser user in users)
            {
                var roles = userManager.GetRoles(user.Id);
                string persona;
                if (user.USER_PERSONA_ID != null)
                {
                    persona = context.Personas.Find(user.USER_PERSONA_ID).PERSONA_NOMBRE_COMPLETO;
                }
                else
                {
                    persona = "";
                }
                
                usuarios.Add(new UserViewModel()
                {
                    Id = user.Id,
                    RoleName = string.Join(", ", roles),
                    UserName = user.UserName,
                    Email = user.Email,
                    Persona = persona
                });

            }

            ViewBag.error = Session["error"];
            return View(usuarios.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ALUMNOS = new SelectList(new PersonasRepository().GetAlumnos(), "ID", "PERSONA_NOMBRE_COMPLETO");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewUserViewModel newUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var repo = new UserRepository();
                if (repo.GetUserByPersonaId(newUserViewModel.USER_PERSONA_ID) != null)
                {
                    Session["error"] = "Ya existe un usuario con esa persona asignada";
                    return RedirectToAction("Index");
                }
                var user = repo.CreateUser(newUserViewModel);
                return RedirectToAction("Index");
            }
            ViewBag.ALUMNOS = new SelectList(new PersonasRepository().GetAlumnos(), "ID", "PERSONA_NOMBRE_COMPLETO");
            return View(newUserViewModel); 

        }
    }
}