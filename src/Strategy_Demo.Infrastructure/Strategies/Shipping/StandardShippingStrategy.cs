using Strategy_Demo.Domain.Entities;
using Strategy_Demo.Domain.Interfaces.Strategies;

namespace Strategy_Demo.Infrastructure.Strategies.Shipping;

public class StandardShippingStrategy : IShippingStrategy
{
    private const double CostPerKg = 1.0;
    private const double CostPerKm = 0.5;

    public double Calculate(Order order) => order.Weight * CostPerKg + order.Distance * CostPerKm;
}