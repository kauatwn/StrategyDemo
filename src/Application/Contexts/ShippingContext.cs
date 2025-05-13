using Application.Abstractions.Contexts;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;

namespace Application.Contexts;

public class ShippingContext(IShippingStrategyFactory factory) : IShippingContext
{
    private IShippingStrategy? _strategy;

    public void SetStrategy(ShippingMethod method)
    {
        _strategy = factory.Create(method)
            ?? throw new InvalidOperationException($"No shipping strategy found for method '{method}'.");
    }

    public double CalculateShippingCost(Order order)
    {
        if (_strategy is null)
            throw new InvalidOperationException("Shipping strategy not set. Call SetStrategy first.");

        return _strategy.Calculate(order);
    }
}