using Application.Abstractions.Contexts;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;

namespace Application.Contexts;

public class ShippingContext(IShippingStrategyFactory factory) : IShippingContext
{
    private IShippingStrategyFactory Factory { get; } = factory;
    public IShippingStrategy Strategy { get; private set; } = null!;

    public void SetStrategy(ShippingMethod method)
    {
        Strategy = Factory.Create(method);
    }

    public double CalculateShippingCost(Order order)
    {
        return Strategy.Calculate(order);
    }
}