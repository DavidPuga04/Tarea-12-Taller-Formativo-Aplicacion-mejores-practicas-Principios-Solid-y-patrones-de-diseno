using DesignPatterns.ModelBuilders;
using DesignPatterns.Models;

namespace DesignPatterns.FactoryMethods
{
    public class ExplorerFactory : CarFactory
    {
        public override Vehicle Create()
        {
            return new CarBuilder()
                .SetColor("Red")
                .SetBrand("Ford")
                .SetModel("Explorer")
                .Build();
        }
    }
}