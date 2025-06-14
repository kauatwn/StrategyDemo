using Strategy_Demo.Domain.Entities;
using Strategy_Demo.Domain.Enums;
using Strategy_Demo.Infrastructure.Strategies.Shipping;

namespace Strategy_Demo.Infrastructure.Tests.Unit.Strategies.Shipping;

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