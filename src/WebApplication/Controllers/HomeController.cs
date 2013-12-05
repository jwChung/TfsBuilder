using System.IO;
using System.Web.Mvc;
using MarkdownSharp;

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
            string markdown;
            using (var reader = new StreamReader(Server.MapPath("~/bin/README.md")))
            {
                markdown = reader.ReadToEnd();
            }

            var html = new Markdown().Transform(markdown);
            ViewBag.Body = html;

            return View();
        }
	}
}