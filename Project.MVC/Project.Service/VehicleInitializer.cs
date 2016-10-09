using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleInitializer : DropCreateDatabaseIfModelChanges<VehicleContext>
    {
        protected override void Seed(VehicleContext context)
        {
            var VehicleMake = new List<VehicleMake>
            {
                new VehicleMake {Name = "Audi", Abrv="aud" },
                new VehicleMake {Name = "BMW", Abrv="bmw" },
                new VehicleMake {Name = "Volkswagen", Abrv="vw" },
                new VehicleMake {Name = "Ferrari", Abrv="fer" },
                new VehicleMake {Name = "Porsche", Abrv="por" },
                new VehicleMake {Name = "Nissan", Abrv="nis" }
            };

            var VehicleModel = new List<VehicleModel>
            {
                new VehicleModel {Name = "R8", MakeId = 1, Abrv = "r8" },
                new VehicleModel {Name = "GTR", MakeId = 6, Abrv = "gtr" },
                new VehicleModel {Name = "f80", MakeId = 1, Abrv = "Enzo" }
            };

            foreach (var Make in VehicleMake)
            {
                context.VehicleMakes.Add(Make);
            }

            foreach (var Model in VehicleModel)
            {
                context.VehicleModels.Add(Model);
            }

            context.SaveChanges();
        }
    }
}
