using System.Web.Mvc;
using System.Web.Routing;

namespace Jwc.TfsBuilder.WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HomeIndex",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "BuildRoute",
                url: "api/{account}/{teamProject}/{definitionName}",
                defaults: new { controller = "TfsBuilder", action = "Build" });
        }
    }
}