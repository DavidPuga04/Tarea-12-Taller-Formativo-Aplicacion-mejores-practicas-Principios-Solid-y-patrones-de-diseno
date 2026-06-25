using DesignPatterns.ModelBuilders;
using DesignPatterns.Models;

namespace DesignPatterns.FactoryMethods
{
    public class FordEscapeFactory : CarFactory
    {
        public override Vehicle Create()
        {
            return new CarBuilder()
                .SetColor("Red")
                .SetBrand("Ford")
                .SetModel("Escape")
                .Build();
        }
    }
}