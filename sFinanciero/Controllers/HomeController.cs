using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sFinanciero.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            ViewBag.Title = "Home Page";
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Application start on :" + DateTime.Now.ToShortTimeString());
            
            return View();
        }
    }
}
