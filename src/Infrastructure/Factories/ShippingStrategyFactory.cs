using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;

namespace Infrastructure.Factories;

public class ShippingStrategyFactory(Func<ShippingMethod, IShippingStrategy> resolver) : IShippingStrategyFactory
{
    public IShippingStrategy Create(ShippingMethod method) => resolver(method);
}