using StrategyDemo.Domain.Entities;
using StrategyDemo.Domain.Interfaces.Strategies;

namespace StrategyDemo.Infrastructure.Strategies.Shipping;

public sealed class ExpressShippingStrategy : IShippingStrategy
{
    private const double CostPerKg = 2.0;
    private const double CostPerKm = 1.0;

    public double Calculate(Order order) => order.Weight * CostPerKg + order.Distance * CostPerKm;
}