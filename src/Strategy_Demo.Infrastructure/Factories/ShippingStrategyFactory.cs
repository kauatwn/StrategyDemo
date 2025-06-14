using Strategy_Demo.Domain.Enums;
using Strategy_Demo.Domain.Interfaces.Factories;
using Strategy_Demo.Domain.Interfaces.Strategies;

namespace Strategy_Demo.Infrastructure.Factories;

public class ShippingStrategyFactory(Func<ShippingMethod, IShippingStrategy> resolver) : IShippingStrategyFactory
{
    public IShippingStrategy Create(ShippingMethod method) => resolver(method);
}