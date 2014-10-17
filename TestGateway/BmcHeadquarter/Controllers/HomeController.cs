namespace BmcHeadquarter.Controllers
{
    using System;
    using System.Web.Mvc;
    using Headquarter.Messages;
    using NServiceBus;

    public class HomeController : Controller
    {
        public IBus Bus { get; set; }

        public ActionResult Index()
        {
            ViewBag.Message = string.Format("Welcome to Headquarter! - Bus is '{0}'", Bus.GetType().FullName);

            return View();
        }

        public ActionResult SendMessageToSiteA()
        {
            Bus.SendToSites(new[] {"SiteA"}, new UpdatePrice {ProductId = 1, NewPrice = 100.0, ValidFrom = DateTime.Now});

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}