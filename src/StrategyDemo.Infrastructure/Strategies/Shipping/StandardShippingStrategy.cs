using StrategyDemo.Domain.Entities;
using StrategyDemo.Domain.Interfaces.Strategies;

namespace StrategyDemo.Infrastructure.Strategies.Shipping;

public class StandardShippingStrategy : IShippingStrategy
{
    private const double CostPerKg = 1.0;
    private const double CostPerKm = 0.5;

    public double Calculate(Order order) => order.Weight * CostPerKg + order.Distance * CostPerKm;
}