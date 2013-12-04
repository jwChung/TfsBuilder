using System.Web.Http;

namespace Jwc.TfsBuilder.WebApplication
{
    /// <summary>
    /// Represents web api configuration.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers web api configuration.
        /// </summary>
        /// <param name="config">The http configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
