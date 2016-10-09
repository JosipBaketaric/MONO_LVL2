using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace Project.Service
{
    public interface VehicleInterface
    {
        void Add(VehicleModel vehicleModel, VehicleMake vehicleMake);    //If there is no that maker then make new one
        void Add(VehicleModel vehicleModel);
        void Edit(VehicleModel vehicleModel);
        void Remove(int Id);
        IEnumerable GetVehicles();
        VehicleModel FindById(int Id);
        VehicleMake FindByIdMake(int Id);
        VehicleMake FindVehicleMakesByName(string Name);
    }
}
