using StrategyDemo.Domain.Entities;
using StrategyDemo.Domain.Enums;
using StrategyDemo.Domain.Interfaces.Factories;
using StrategyDemo.Domain.Interfaces.Strategies;
using StrategyDemo.Application.Interfaces.Contexts;

namespace StrategyDemo.Application.Contexts;

public sealed class ShippingContext(IShippingStrategyFactory factory) : IShippingContext
{
    private IShippingStrategy? _strategy;

    public void SetStrategy(ShippingMethod method) => _strategy = factory.Create(method);

    public double CalculateShippingCost(Order order)
    {
        return _strategy?.Calculate(order) ??
               throw new InvalidOperationException("Shipping strategy not set. Call SetStrategy first.");
    }
}