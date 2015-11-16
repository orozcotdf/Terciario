using System;
using ColegioTerciario.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using ColegioTerciario.Areas.Admin.Models;
using ColegioTerciario.Models.Repositories;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ColegioTerciario.Lib;
using ColegioTerciario.Models.User;
using MvcFlash.Core;
using MvcFlash.Core.Extensions;
using MvcFlash.Core.Filters;

namespace ColegioTerciario.Areas.Admin.Controllers
{
    public class UsuariosController : AdminController
    {
        private UserRepository _repo;
        private ColegioTerciarioContext context;

        public UsuariosController()
        {
            context = GetContext();
            _repo = new UserRepository();
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

        private ApplicationRoleManager roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this.roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this.roleManager = value; }
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
            ViewBag.ROLES = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            ViewBag.ALUMNOS = new SelectList(new PersonasRepository().GetAlumnos(), "ID", "PERSONA_NOMBRE_COMPLETO");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewUserViewModel newUserViewModel)
        {
            ViewBag.ROLES = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            if (ModelState.IsValid)
            {
                var repo = new UserRepository();
                if (repo.GetUserByPersonaId(newUserViewModel.USER_PERSONA_ID) != null)
                {
                    Session["error"] = "Ya existe un usuario con esa persona asignada";

                    return RedirectToAction("Index");
                }
                int personaId = repo.GetPersonaIdFromDni(newUserViewModel.UserName);
                if (personaId == 0)
                {
                    ModelState.AddModelError("UserName", "No existe ninguna persona con este documento.");
                    return View(newUserViewModel); 
                }
                newUserViewModel.USER_PERSONA_ID = personaId;
                string userId = repo.CreateUser(newUserViewModel);
                if (userId == null)
                {
                    ModelState.AddModelError("UserName", "Ya existe un usuario con este documento.");
                    return View(newUserViewModel); 
                }
                string code = UserManager.GeneratePasswordResetToken(userId);
                string encodedCode = code.Base64ForUrlEncode();
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId, code = encodedCode, area = "" }, protocol: Request.Url.Scheme);

                Mailer.SendMailWithOffice365(newUserViewModel.Email, newUserViewModel.UserName, callbackUrl);

                Flash.Instance.Success("Usuario Creado", "El usuario debera revisar su correo para activar su cuenta.");

                return RedirectToAction("Index");
            }
            return View(newUserViewModel); 

        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ALUMNOS = new SelectList(new PersonasRepository().GetAlumnos(), "ID", "PERSONA_NOMBRE_COMPLETO");
            ApplicationUser user = _repo.GetUser(id);


            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userRoles = UserManager.GetRoles(id);
            ViewBag.USER_ROLES = roleStore.Roles.ToList().Select(x => new SelectListItem()
            {
                Selected = userRoles.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
            });
            EditUserViewModel vm = new EditUserViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                USER_PERSONA_ID = user.USER_PERSONA_ID != null ? user.USER_PERSONA_ID.Value : 0,
            };
            
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel vm, params string[] selectedRoles)
        {
            ViewBag.ALUMNOS = new SelectList(new PersonasRepository().GetAlumnos(), "ID", "PERSONA_NOMBRE_COMPLETO");
            var userRoles = UserManager.GetRoles(vm.ID);

            ViewBag.USER_ROLES = RoleManager.Roles.ToList().Select(x => new SelectListItem()
            {
                Selected = userRoles.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
            });
            if (ModelState.IsValid)
            {
                var db = new ColegioTerciarioContext();
                var usuario = db.Users.Find(vm.ID);
                usuario.USER_PERSONA_ID = vm.USER_PERSONA_ID;
                usuario.UserName = vm.UserName;
                if (usuario.Email != vm.Email)
                {
                    usuario.Email = vm.Email;
                };

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (vm.USER_ROLES != null)
                {
                    var rolesToDelete = (from r in roleManager.Roles
                                         where !vm.USER_ROLES.Contains(r.Name)
                                         select r.Name).ToArray();
                    var removeFromRoles = UserManager.RemoveFromRoles(vm.ID, rolesToDelete);
                    foreach (var role in vm.USER_ROLES)
                    {
                        if (!UserManager.GetRoles(vm.ID).Contains(role))
                        {
                            var addtorole = UserManager.AddToRole(vm.ID, role);
                        }
                    }
                }
                else
                {
                    UserManager.RemoveFromRoles(vm.ID, roleManager.Roles.Select(r => r.Name).ToArray());
                }
                db.SaveChanges();

                if (vm.Password != null)
                {
                    UserManager.RemovePassword(usuario.Id);
                    var result = UserManager.AddPassword(usuario.Id, vm.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    
                    AddErrors(result);
                    return View(vm);
                }
                

                return RedirectToAction("Index");
            }
            else
            {
                
                return View(vm);
            }
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}