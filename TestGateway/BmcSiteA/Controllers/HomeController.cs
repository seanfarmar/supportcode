namespace BmcSiteA.Controllers
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
            ViewBag.Message = "Welcome to Site A!" + Bus.GetType().FullName;

            return View();
        }

        public ActionResult SendMessageToHO()
        {
            Bus.SendToSites(new[] {"HO"},
                    new UpdatePrice {ProductId = 1, NewPrice = 100.0, ValidFrom = DateTime.Now});
            
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}