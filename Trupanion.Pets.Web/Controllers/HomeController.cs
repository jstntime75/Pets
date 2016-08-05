using System.Web.Mvc;

namespace Trupanion.Pets.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}