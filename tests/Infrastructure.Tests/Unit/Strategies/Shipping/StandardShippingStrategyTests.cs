using Domain.Entities;
using Domain.Enums;
using Infrastructure.Strategies.Shipping;

namespace Infrastructure.Tests.Unit.Strategies.Shipping;

public class StandardShippingStrategyTests
{
    private const double CostPerKg = 1.0;
    private const double CostPerKm = 0.5;

    private readonly StandardShippingStrategy _strategy = new();

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