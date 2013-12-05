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
            ViewBag.Body = GetHtmlFromMarkdown(GetMarkdownContent());
            return View();
        }

        private static string GetHtmlFromMarkdown(string markdown)
        {
            return new Markdown().Transform(markdown);
        }

        private string GetMarkdownContent()
        {
            using (var reader = new StreamReader(Server.MapPath("~/bin/README.md")))
            {
                return reader.ReadToEnd();
            }
        }
    }
}