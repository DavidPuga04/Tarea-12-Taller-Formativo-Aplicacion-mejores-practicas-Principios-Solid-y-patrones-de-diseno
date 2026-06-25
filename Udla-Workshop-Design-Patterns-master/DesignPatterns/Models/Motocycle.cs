using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns.Models
{
    public class Motocycle : Vehicle
    {
        public override int Tires { get => 2; }

        public Motocycle(string color, string brand, string model) : base(color,brand,model, 5)
        {

        }
    }
}
