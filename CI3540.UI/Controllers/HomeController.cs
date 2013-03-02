using System.Web.Mvc;

namespace CI3540.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Index Page";

            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "About Page";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Page";

            return View();
        }
    }
}
