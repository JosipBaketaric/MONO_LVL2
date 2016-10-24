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
        private VehicleContext db;
        private const int PAGE_SIZE = 5;
        
        private VehicleService() {
            this.db = new VehicleContext();
        }
        public static VehicleService GetInstance()
        {
            if (instance == null)
                instance = new VehicleService();
            return instance;
        }


        public bool Add(VehicleModel vehicleModel)
        {
            var result = (from r in this.db.VehicleMakes where r.VehicleMakeId == vehicleModel.VehicleMakeId select r).FirstOrDefault();

            if(result != null)
            {
                this.db.VehicleModels.Add(vehicleModel);
                this.db.SaveChanges();
                return true;
            }

            return false;        
        }

        public void Add(VehicleMake vehicleMake)
        {
            this.db.VehicleMakes.Add(vehicleMake);
            this.db.SaveChanges();
        }

        public void Edit(VehicleModel vehicleModel)
        {
            this.db.Entry(vehicleModel).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Edit(VehicleMake vehicleMake)
        {
            this.db.Entry(vehicleMake).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public VehicleModel FindByIdModel(int? Id)
        {
            var result = (from r in this.db.VehicleModels where r.VehicleModelId == Id select r).FirstOrDefault();
            return result;
        }
        public VehicleMake FindByIdMake(int? Id)
        {
            var result = (from r in this.db.VehicleMakes where r.VehicleMakeId == Id select r).FirstOrDefault();
            return result;
        }     

        public IEnumerable GetVehicleModels(string search, int? page, string sortBy)
        {
            IEnumerable returnValue;
            List<VehicleMake> vehicleMakersList = this.db.VehicleMakes.ToList();

            var Models = this.db.VehicleModels.AsQueryable();             

            if (search == null || search.Equals(""))
            {
                Models = this.db.VehicleModels.AsQueryable();
            }
            else
            {
                Models = this.db.VehicleModels.Where(x => x.Name.StartsWith(search)).AsQueryable();
            }

            switch (sortBy)
            {
                case "Name desc":
                    returnValue = Models.OrderByDescending(x => x.Name).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Id desc":
                    returnValue = Models.OrderByDescending(x => x.VehicleModelId).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Name":
                    returnValue = Models.OrderBy(x => x.Name).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "MakerName":
                    returnValue = Models.OrderBy(x => x.VehicleMake.Name ).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "MakerName desc":
                    returnValue = Models.OrderByDescending(x => x.VehicleMake.Name).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;

                default:
                    returnValue = Models.OrderBy(x => x.VehicleModelId).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
            }

            return returnValue;
        }

        //PAGING AND SEARCH
        public IEnumerable GetVehicleMakes(string search, int? page, string sortBy)
        {
            IEnumerable returnValue;

            var Makes = this.db.VehicleMakes.AsQueryable();

            if (search == null || search.Equals(""))
            {
                Makes = this.db.VehicleMakes.AsQueryable();
            }
            else
            {
                Makes = this.db.VehicleMakes.Where(x => x.Name.StartsWith(search)).AsQueryable();
            }

            switch (sortBy)
            {
                case "Name desc":
                    returnValue = Makes.OrderByDescending(x => x.Name).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Id desc":
                    returnValue = Makes.OrderByDescending(x => x.VehicleMakeId).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                case "Name":
                    returnValue = Makes.OrderBy(x => x.Name).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
                default:
                    returnValue = Makes.OrderBy(x => x.VehicleMakeId).ToPagedList(page ?? 1, PAGE_SIZE);
                    break;
            }

            return returnValue;
        }

        //NOT PAGED
        public IEnumerable GetVehicleMakesAll()
        {
            IEnumerable<VehicleMake> vehicleMakesList;
            vehicleMakesList = this.db.VehicleMakes.ToList();
            return vehicleMakesList;
        }

        public void RemoveModel(int? Id)
        {
            VehicleModel vm = this.db.VehicleModels.Find(Id);
            this.db.VehicleModels.Remove(vm);
            this.db.SaveChanges();
        }

        public bool RemoveMake(int? Id)
        {
            VehicleMake vm = this.db.VehicleMakes.Find(Id);

            var result = (from r in this.db.VehicleModels where r.VehicleMakeId == Id select r).FirstOrDefault();

            if(result == null)
            {
                this.db.VehicleMakes.Remove(vm);
                this.db.SaveChanges();
                return true;
            }

            return false;
                                       
        }

    }
}
