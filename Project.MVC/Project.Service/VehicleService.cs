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


        public void Add(VehicleModel vehicleModel)
        {
            VehicleContext db = new VehicleContext();
            db.VehicleModels.Add(vehicleModel);
            db.SaveChanges();
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

        //Delete
        public VehicleMake FindVehicleMakesByName(string Name)
        {
            VehicleContext db = new VehicleContext();
            return (from r in db.VehicleMakes where r.Name.ToUpper() == Name.ToUpper() select r).FirstOrDefault();
        }
        //Model + Make
        public IEnumerable GetVehicles()
        {
            VehicleContext db = new VehicleContext();
            List<VehicleRepository> vehicleRepository = new List<VehicleRepository>();

            foreach(var temp in db.VehicleModels)
            {
                vehicleRepository.Add( new VehicleRepository { vehicleModel = temp, vehicleMake = FindByIdMake(temp.MakeId) } );
            }

            return vehicleRepository;
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
            VehicleMake vm = db.VehicleMakes.Find(Id);

            var result = (from r in db.VehicleModels where r.Id == Id select r).FirstOrDefault();

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
