using Domain.Entities;
using Domain.Enums;
using Infrastructure.Strategies.Shipping;

namespace Infrastructure.UnitTests.Strategies.Shipping;

public class StandardShippingStrategyTests
{
    private const double CostPerKg = 1.0;
    private const double CostPerKm = 0.5;

    private StandardShippingStrategy Strategy { get; } = new();

    [Fact]
    public void ShouldCalculateShippingCostBasedOnWeightAndDistance()
    {
        // Arrange
        var order = new Order(10, 100, ShippingMethod.Standard);
        double expected = order.Weight * CostPerKg + order.Distance * CostPerKm;

        // Act
        double result = Strategy.Calculate(order);

        // Assert
        Assert.Equal(expected, result);
    }
}