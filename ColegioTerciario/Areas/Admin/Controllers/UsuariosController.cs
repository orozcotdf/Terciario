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
                usuarios.Add(new UserViewModel()
                {
                    Id = user.Id,
                    RoleName = string.Join(", ", roles),
                    UserName = user.UserName,
                    Email = user.Email
                });

            }
            return View(usuarios.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewUserViewModel newUserViewModel)
        {
            var user = new ApplicationUser();
            
            return View();
        }
    }
}