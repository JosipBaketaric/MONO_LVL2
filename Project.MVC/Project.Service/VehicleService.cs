using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleService
    {
        private static VehicleService instance = null;
        
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

        public IEnumerable GetVehicleModels()
        {
            VehicleContext db = new VehicleContext();
            IEnumerable<VehicleModel> vehicleModelList = db.VehicleModels.ToList();
            return vehicleModelList;
        }

        public IEnumerable GetVehicleMakes()
        {
            VehicleContext db = new VehicleContext();
            IEnumerable<VehicleMake> vehicleMakesList = db.VehicleMakes.ToList();
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
            VehicleMake vm = FindByIdMake(Id);

            var result = (from r in db.VehicleModels where r.MakeId == Id select r).FirstOrDefault();

            if(result == null)
            {
                db.VehicleMakes.Remove(vm);
                db.SaveChanges();
                return true;
            }

            return false;
                                       
        }

        public IEnumerable<VehicleMake> filterMake(string search)
        {
            if (search == null)
                return null;

            VehicleContext db = new VehicleContext();
            IEnumerable<VehicleMake> vehicleMakesList;

            if (search.Equals(""))
            {
                vehicleMakesList = db.VehicleMakes.ToList();
                return vehicleMakesList;
            }


            vehicleMakesList = db.VehicleMakes.Where(x => x.Name.StartsWith(search)).ToList();

            return vehicleMakesList;
        }

        public IEnumerable<VehicleModel> filterModel(string search)
        {
            if (search == null)
                return null;

            VehicleContext db = new VehicleContext();
            IEnumerable<VehicleModel> vehicleModelList;

            if (search.Equals(""))
            {
                vehicleModelList = db.VehicleModels.ToList();
                return vehicleModelList;
            }


            vehicleModelList = db.VehicleModels.Where(x => x.Name.StartsWith(search)).ToList();

            return vehicleModelList;
        }

    }
}
