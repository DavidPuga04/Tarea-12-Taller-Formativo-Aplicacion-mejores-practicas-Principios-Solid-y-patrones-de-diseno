using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns.Models
{
    public interface IVehicle
    {
        void StartEngine();

        void StopEngine();

        void AddGas();

        bool NeedsGas();

        bool IsEngineOn();
    }
}
