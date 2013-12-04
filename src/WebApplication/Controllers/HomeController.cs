using System.Web.Mvc;

namespace Jwc.TfsBuilder.WebApplication.Controllers
{
    /// <summary>
    /// Represents the home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Views the default page.
        /// </summary>
        public ActionResult Index()
        {
            return View("Index");
        }
	}
}