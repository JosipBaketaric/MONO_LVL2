using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    interface VehicleServiceInterface
    {
        bool Add(VehicleModel vehicleModel);
        void Add(VehicleMake vehicleMake);
        void Edit(VehicleModel vehicleModel);
        void Edit(VehicleMake vehicleMake);
        VehicleModel FindByIdModel(int? Id);
        VehicleMake FindByIdMake(int? Id);
        IEnumerable GetVehicleModels(string search, int? page, string sortBy);
        IEnumerable GetVehicleMakes(string search, int? page, string sortBy);
        IEnumerable GetVehicleMakesAll();
        void RemoveModel(int? Id);
        bool RemoveMake(int? Id);

    }
}
