using Domain.Entities;
using Domain.Interfaces.Strategies;

namespace Infrastructure.Strategies.Shipping;

public class StandardShippingStrategy : IShippingStrategy
{
    private const double CostPerKg = 1.0;
    private const double CostPerKm = 0.5;

    public double Calculate(Order order)
    {
        return order.Weight * CostPerKg + order.Distance * CostPerKm;
    }
}