using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

namespace Project.Service
{
    public class VehicleService
    {
        private static VehicleService instance = null;
        private const int PAGE_SIZE = 5;
        
        private VehicleService() { }
        public static VehicleService GetInstance()
        {
            if (instance == null)
                instance = new VehicleService();
            return instance;
        }


        public bool Add(VehicleModel vehicleModel)
        {
            VehicleContext db = new VehicleContext();

            var result = (from r in db.VehicleMakes where r.Id == vehicleModel.MakeId select r).FirstOrDefault();

            if(result != null)
            {
                db.VehicleModels.Add(vehicleModel);
                db.SaveChanges();
                return true;
            }

            return false;        
        }

        public void Add(VehicleMake vehicleMake)
        {
            VehicleContext db = new VehicleContext();
            db.VehicleMakes.Add(vehicleMake);
            db.SaveChanges();
        }

        public void Edit(VehicleModel vehicleModel)
        {
            VehicleContext db = new VehicleContext();
            db.Entry(vehicleModel).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Edit(VehicleMake vehicleMake)
        {
            VehicleContext db = new VehicleContext();
            db.Entry(vehicleMake).State = EntityState.Modified;
            db.SaveChanges();
        }

        public VehicleModel FindByIdModel(int? Id)
        {
            VehicleContext db = new VehicleContext();
            var result = (from r in db.VehicleModels where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public VehicleMake FindByIdMake(int? Id)
        {
            VehicleContext db = new VehicleContext();
            var result = (from r in db.VehicleMakes where r.Id == Id select r).FirstOrDefault();
            return result;
        }     

        public IEnumerable GetVehicleModels(string search, int? page, string sortBy)
        {
            VehicleContext db = new VehicleContext();
            IEnumerable returnValue;

            var Models = db.VehicleModels.AsQueryable();             

            if (search == null || search.Equals(""))
            {
                Models = db.VehicleModels.AsQueryable();
            }
            else
            {
                Models = db.VehicleModels.Where(x => x.Name.StartsWith(search)).AsQueryable();
            }

            switch (sortBy)
            {
                case "Name desc":
                    returnValue = Models.OrderByDescending(x => x.Name).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Id desc":
                    returnValue = Models.OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Name":
                    returnValue = Models.OrderBy(x => x.Name).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                default:
                    returnValue = Models.OrderBy(x => x.Id).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
            }

            return returnValue;
        }

        //PAGING AND SEARCH
        public IEnumerable GetVehicleMakes(string search, int? page, string sortBy)
        {
            VehicleContext db = new VehicleContext();
            IEnumerable returnValue;

            var Makes = db.VehicleMakes.AsQueryable();

            if (search == null || search.Equals(""))
            {
                Makes = db.VehicleMakes.AsQueryable();
            }
            else
            {
                Makes = db.VehicleMakes.Where(x => x.Name.StartsWith(search)).AsQueryable();
            }

            switch (sortBy)
            {
                case "Name desc":
                    returnValue = Makes.OrderByDescending(x => x.Name).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Id desc":
                    returnValue = Makes.OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Name":
                    returnValue = Makes.OrderBy(x => x.Name).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                default:
                    returnValue = Makes.OrderBy(x => x.Id).ToList().ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
            }

            return returnValue;
        }

        //NOT PAGED
        public IEnumerable GetVehicleMakesAll()
        {
            VehicleContext db = new VehicleContext();
            IEnumerable<VehicleMake> vehicleMakesList;
            vehicleMakesList = db.VehicleMakes.ToList();
            return vehicleMakesList;
        }

        public void RemoveModel(int? Id)
        {
            VehicleContext db = new VehicleContext();
            VehicleModel vm = db.VehicleModels.Find(Id);
            db.VehicleModels.Remove(vm);
            db.SaveChanges();
        }

        public bool RemoveMake(int? Id)
        {
            VehicleContext db = new VehicleContext();
            VehicleMake vm = db.VehicleMakes.Find(Id);

            var result = (from r in db.VehicleModels where r.MakeId == Id select r).FirstOrDefault();

            if(result == null)
            {
                db.VehicleMakes.Remove(vm);
                db.SaveChanges();
                return true;
            }

            return false;
                                       
        }

    }
}
