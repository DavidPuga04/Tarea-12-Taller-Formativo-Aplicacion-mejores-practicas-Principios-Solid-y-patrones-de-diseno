using DesignPatterns.Models;
using DesignPatterns.Infraestructure.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Repositories
{
    public class MyVehiclesRepository : IVehicleRepository
    {
        // Uso del Singleton para mantener datos en memoria
        private readonly MemoryCollection _memoryCollection =
            MemoryCollection.Instance;

        public void AddVehicle(Vehicle vehicle)
        {
            _memoryCollection.Vehicles.Add(vehicle);
        }

        public Vehicle Find(string id)
        {
            return _memoryCollection.Vehicles
                .FirstOrDefault(v => v.ID.Equals(new Guid(id)));
        }

        public ICollection<Vehicle> GetVehicles()
        {
            return _memoryCollection.Vehicles;
        }
    }
}