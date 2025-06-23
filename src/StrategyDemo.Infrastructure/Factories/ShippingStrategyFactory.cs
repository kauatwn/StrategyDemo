using StrategyDemo.Domain.Enums;
using StrategyDemo.Domain.Interfaces.Factories;
using StrategyDemo.Domain.Interfaces.Strategies;

namespace StrategyDemo.Infrastructure.Factories;

public class ShippingStrategyFactory(Func<ShippingMethod, IShippingStrategy> resolver) : IShippingStrategyFactory
{
    public IShippingStrategy Create(ShippingMethod method) => resolver(method);
}