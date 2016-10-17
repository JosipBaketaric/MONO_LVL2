using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Service;
using System.Net;

namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {
        private VehicleService vehicleService = VehicleService.GetInstance();
        // GET: Make

        public ActionResult Index(string search, int? page, string sortBy)
        {
            ViewBag.SortByIdParameter = string.IsNullOrEmpty(sortBy) ? "Id desc" : "";
            ViewBag.SortByNameParameter = sortBy == "Name" ? "Name desc" : "Name";

            return View( vehicleService.GetVehicleMakes(search, page, sortBy) );
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "VehicleMakeId,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                vehicleService.Add(vehicleMake);
                return RedirectToAction("Index");
            }
            return View(vehicleMake);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            VehicleMake vehicleMake = vehicleService.FindByIdMake(id);

            if (vehicleMake == null)
                return HttpNotFound();

            return View(vehicleMake);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleMakeId,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                vehicleService.Edit(vehicleMake);
                return RedirectToAction("Index");
            }

            return View(vehicleMake);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            VehicleMake vehicleMake = vehicleService.FindByIdMake(id);

            if (vehicleMake == null)
                return HttpNotFound();

            return View(vehicleMake);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(vehicleService.FindByIdMake(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ( vehicleService.RemoveMake(id) == true )
                return RedirectToAction("Index");
            ViewBag.Error = "Couldn't delete make. There are stil models that are using it.";
            return View("Index", vehicleService.GetVehicleMakes("",1, "") );
        }

    }
}