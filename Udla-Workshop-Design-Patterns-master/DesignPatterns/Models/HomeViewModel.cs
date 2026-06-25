using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns.Models
{
    public class HomeViewModel
    {
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
