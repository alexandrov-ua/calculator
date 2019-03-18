using System.Web.Mvc;

namespace Calculator.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}