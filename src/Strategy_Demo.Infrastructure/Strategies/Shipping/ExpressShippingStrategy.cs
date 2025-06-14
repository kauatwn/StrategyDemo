using Strategy_Demo.Domain.Entities;
using Strategy_Demo.Domain.Interfaces.Strategies;

namespace Strategy_Demo.Infrastructure.Strategies.Shipping;

public class ExpressShippingStrategy : IShippingStrategy
{
    private const double CostPerKg = 2.0;
    private const double CostPerKm = 1.0;

    public double Calculate(Order order) => order.Weight * CostPerKg + order.Distance * CostPerKm;
}