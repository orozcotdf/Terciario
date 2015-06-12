using System.Web.Mvc;
using System.Web.Routing;

namespace ColegioTerciario
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "EditarAsistencia",
                url: "Curso/{cursoId}/Asistencias/Edit/{string:fecha}",
                defaults: new { controller = "Asistencias", action = "Edit" }
            );

            routes.MapRoute(
                name: "Asistencias",
                url: "Curso/{cursoId}/Asistencias/{action}/{id}",
                defaults: new { controller = "Asistencias", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Cursos",
                url: "Curso/{ciclo}/{nombre}/{sedeId}",
                defaults: new { controller = "Cursos", action = "editarCurso"}
            );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
            
        }
    }
}
