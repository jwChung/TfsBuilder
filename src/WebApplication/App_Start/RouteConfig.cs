using System.Web.Mvc;
using System.Web.Routing;

namespace Jwc.TfsBuilder.WebApplication
{
    /// <summary>
    /// Represents route configurations.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    public class RouteConfig
    {
        /// <summary>
        /// Registers routes.
        /// </summary>
        /// <param name="routes">Routes to be registered.</param>
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