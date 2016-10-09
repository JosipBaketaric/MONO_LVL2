using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleService : VehicleInterface
    {
        private static VehicleContext db = new VehicleContext();
        private static VehicleService instance = null;

        private VehicleService() { }
        public static VehicleService GetInstance()
        {
            if (instance == null)
                instance = new VehicleService();
            return instance;
        }
        public void Add(VehicleModel vehicleModel, VehicleMake vehicleMake)
        {
            if (FindVehicleMakesByName(vehicleMake.Name) == null)
            {
                db.VehicleMakes.Add(vehicleMake);
            }   
            
            db.VehicleModels.Add(vehicleModel);
            db.SaveChanges();
        }

        public void Add(VehicleModel vehicleModel)
        {
            db.VehicleModels.Add(vehicleModel);
            db.SaveChanges();
        }

        public void Edit(VehicleModel vehicleModel)
        {
            db.Entry(vehicleModel).State = System.Data.Entity.EntityState.Modified;            
        }

        public VehicleModel FindById(int Id)
        {
            var result = (from r in db.VehicleModels where r.Id == Id select r).FirstOrDefault();
            return result;
        }
        public VehicleMake FindByIdMake(int Id)
        {
            var result = (from r in db.VehicleMakes where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public VehicleMake FindVehicleMakesByName(string Name)
        {
            return (from r in db.VehicleMakes where r.Name.ToUpper() == Name.ToUpper() select r).FirstOrDefault();
        }

        public IEnumerable GetVehicles()
        {
            List<VehicleRepository> vehicleRepository = new List<VehicleRepository>();

            foreach(var temp in db.VehicleModels)
            {
                vehicleRepository.Add( new VehicleRepository { vehicleModel = temp, vehicleMake = FindByIdMake(temp.MakeId) } );
            }

            return vehicleRepository;
        }

        public void Remove(int Id)
        {
            VehicleModel vm = db.VehicleModels.Find(Id);
            db.VehicleModels.Remove(vm);
            db.SaveChanges();
        }

    }
}
