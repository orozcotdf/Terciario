using System.Web.Mvc;
using System.Web.Routing;

namespace ColegioTerciario.Areas.Alumnos
{
    public class AlumnosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Alumnos";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {/*
            context.MapRoute(
                "Alumnos_entrar",
                "Alumnos/Entrar/{returnUrl}",
                new { action = "Sesiones", Action = "Entrar", returnUrl = UrlParameter.Optional }
            );
            */
            context.MapRoute(
                "Alumnos_default",
                "Alumnos/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}