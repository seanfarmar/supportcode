using System.Web.Mvc;

namespace DocumentServices.Saga.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}