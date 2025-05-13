using Domain.Entities;
using Domain.Interfaces.Strategies;

namespace Infrastructure.Strategies.Shipping;

public class ExpressShippingStrategy : IShippingStrategy
{
    private const double CostPerKg = 2.0;
    private const double CostPerKm = 1.0;

    public double Calculate(Order order) => order.Weight * CostPerKg + order.Distance * CostPerKm;
}