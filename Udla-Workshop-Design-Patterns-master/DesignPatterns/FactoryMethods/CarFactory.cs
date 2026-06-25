using DesignPatterns.Models;

namespace DesignPatterns.FactoryMethods
{
    public abstract class CarFactory
    {
        public abstract Vehicle Create();
    }
}