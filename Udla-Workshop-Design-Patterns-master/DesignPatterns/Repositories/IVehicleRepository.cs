using DesignPatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns.Repositories
{
    public interface IVehicleRepository
    {
        ICollection<Vehicle> GetVehicles();

        void AddVehicle(Vehicle vehicle);

        Vehicle Find(string id);

    }
}
