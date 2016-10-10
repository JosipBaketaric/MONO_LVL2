using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Service;
using System.Net;
using System.Data.Entity;

namespace Project.MVC.Controllers
{
    public class MainController : Controller
    {
        private VehicleService vehicleService = VehicleService.GetInstance();

        // GET: Main
        public ActionResult Index()
        {
            return View(vehicleService.GetVehicles());  //Get VehicleRepository object
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            VehicleModel vehicleModel = vehicleService.FindByIdModel(id);
            VehicleMake vehicleMake = vehicleService.FindByIdMake(vehicleModel.MakeId);
            
            if(vehicleMake == null || vehicleModel == null)
                return HttpNotFound();

            VehicleRepository vehicleRepository = new VehicleRepository() { vehicleMake = vehicleMake, vehicleModel = vehicleModel };
            return View(vehicleRepository);
        }

        public ActionResult EditMake(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            VehicleMake vehicleMake = vehicleService.FindByIdMake(id);

            if (vehicleMake == null)
                return HttpNotFound();

            vehicleService.Edit(vehicleMake);

            return View(vehicleMake);
        }

        public ActionResult EditModel(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            VehicleModel vehicleModel = vehicleService.FindByIdModel(id);

            if (vehicleModel == null)
                return HttpNotFound();

            vehicleService.Edit(vehicleModel);

            return View(vehicleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMake([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                vehicleService.Edit(vehicleMake);
                return RedirectToAction("Index");
            }
            
            return View(vehicleMake);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditModel([Bind(Include = "Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                vehicleService.Edit(vehicleModel);
                //VehicleContext db = new VehicleContext();
                //db.Entry(vehicleModel).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleModel);
        }

        public ActionResult DeleteModel (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View( vehicleService.FindByIdModel(id) );
        }

        public ActionResult DeleteMake (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View( vehicleService.FindByIdMake(id) );
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("DeleteModel")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteModelConfirmed(int id)
        {
            vehicleService.RemoveModel(id);
            return RedirectToAction("Index");
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("DeleteMake")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMakeConfirmed(int id)
        {
            if(vehicleService.RemoveMake(id))
                return RedirectToAction("Index");
            return View("DeleteFailed");
        }

        public ActionResult Create()
        {
            ViewBag.List = vehicleService.GetAllVehicleMakers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                vehicleService.Add(vehicleModel);
                return RedirectToAction("Index");
            }
            
            return View(vehicleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMake([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                vehicleService.Add(vehicleMake);
                return RedirectToAction("Index");
            }

            return View(vehicleMake);
        }

    }
}