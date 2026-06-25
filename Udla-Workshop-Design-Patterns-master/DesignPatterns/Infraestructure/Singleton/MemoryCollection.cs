using DesignPatterns.Models;
using System.Collections.Generic;

namespace DesignPatterns.Infraestructure.Singleton
{
    public class MemoryCollection
    {
        // Colección única compartida por toda la aplicación
        public ICollection<Vehicle> Vehicles { get; set; }
            = new List<Vehicle>();

        private static MemoryCollection _instance;

        private MemoryCollection()
        {

        }

        public static MemoryCollection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MemoryCollection();
                }

                return _instance;
            }
        }
    }
}