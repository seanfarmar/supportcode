namespace WebApplication_mvc5.Controllers
{
    using System;
    using System.Web.Mvc;
    using Messages;
    using NServiceBus;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var guid = Guid.NewGuid();

            MvcApplication.Bus.SendLocal<TestMessage>(m => m.Guid = guid);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}