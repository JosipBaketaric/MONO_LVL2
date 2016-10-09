using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Service;

namespace Project.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            VehicleContext db = new VehicleContext();

            var VehicleMakers = db.VehicleMakes.ToList();

            return View(VehicleMakers);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}