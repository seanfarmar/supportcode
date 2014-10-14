using Headquarter.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BmcHeadquarter.Controllers
{
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
            try
            {
                Bus.SendToSites(new[] { "SiteA" }, new UpdatePrice { ProductId = 1, NewPrice = 100.0, ValidFrom = DateTime.Now });
            }
            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}