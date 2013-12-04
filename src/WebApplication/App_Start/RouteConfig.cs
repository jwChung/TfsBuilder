using System.Web.Mvc;
using System.Web.Routing;

namespace Jwc.TfsBuilder.WebApplication
{
    /// <summary>
    /// Represents route configurations.
    /// </summary>
    public static class RouteConfig
    {
        /// <summary>
        /// Registers routes.
        /// </summary>
        /// <param name="routes">Routes to be registered.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "BuildRoute",
                url: "build/{account}/{teamProject}/{definitionName}",
                defaults: new { controller = "TfsBuilder", action = "Build" });
        }
    }
}