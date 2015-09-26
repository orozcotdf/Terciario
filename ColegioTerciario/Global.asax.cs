using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ColegioTerciario.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace ColegioTerciario
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);            

            GlobalFilters.Filters.Add(new UserDataActionFilter(), 0);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class UserDataActionFilter : ActionFilterAttribute
    {
        private ApplicationRoleManager roleManager;

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this.roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this.roleManager = value; }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {                
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var roles = UserManager.GetRoles(userId);
                var userVM = new UserViewModel
                {
                    Id = userId,
                    UserName = HttpContext.Current.User.Identity.Name
                };
                userVM.Roles = new List<RoleViewModel>();
                foreach (string role in roles)
                {
                    userVM.Roles.Add(new RoleViewModel {Name = role});
                }
                filterContext.Controller.ViewBag.UserData = JsonConvert.SerializeObject(userVM);
            }

        } 
        
    }
}
