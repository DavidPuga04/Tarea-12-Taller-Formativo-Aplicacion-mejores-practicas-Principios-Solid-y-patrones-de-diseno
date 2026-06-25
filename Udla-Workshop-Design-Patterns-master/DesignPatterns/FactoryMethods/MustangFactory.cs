using DesignPatterns.ModelBuilders;
using DesignPatterns.Models;

namespace DesignPatterns.FactoryMethods
{
    public class MustangFactory : CarFactory
    {
        public override Vehicle Create()
        {
            return new CarBuilder()
                .SetColor("Red")
                .SetBrand("Ford")
                .SetModel("Mustang")
                .Build();
        }
    }
}