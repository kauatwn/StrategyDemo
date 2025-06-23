using StrategyDemo.Domain.Entities;
using StrategyDemo.Domain.Enums;
using StrategyDemo.Infrastructure.Strategies.Shipping;

namespace StrategyDemo.Infrastructure.Tests.Unit.Strategies.Shipping;

public class ExpressShippingStrategyTests
{
    private const double CostPerKg = 2.0;
    private const double CostPerKm = 1.0;

    private readonly ExpressShippingStrategy _strategy = new();

    [Fact]
    public void ShouldCalculateShippingCostBasedOnWeightAndDistance()
    {
        // Arrange
        Order order = new(weight: 10, distance: 100, shippingMethod: ShippingMethod.Express);
        double expected = order.Weight * CostPerKg + order.Distance * CostPerKm;

        // Act
        double result = _strategy.Calculate(order);

        // Assert
        Assert.Equal(expected, result);
    }
}