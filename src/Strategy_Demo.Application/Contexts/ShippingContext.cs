using Strategy_Demo.Domain.Entities;
using Strategy_Demo.Domain.Enums;
using Strategy_Demo.Domain.Interfaces.Factories;
using Strategy_Demo.Domain.Interfaces.Strategies;
using Strategy_Demo.Application.Interfaces.Contexts;

namespace Strategy_Demo.Application.Contexts;

public class ShippingContext(IShippingStrategyFactory factory) : IShippingContext
{
    private IShippingStrategy? _strategy;

    public void SetStrategy(ShippingMethod method) => _strategy = factory.Create(method);

    public double CalculateShippingCost(Order order)
    {
        if (_strategy is null)
            throw new InvalidOperationException("Shipping strategy not set. Call SetStrategy first.");

        return _strategy.Calculate(order);
    }
}