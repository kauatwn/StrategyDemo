using Application.Contexts;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Infrastructure.Factories;
using Infrastructure.Strategies.Shipping;

namespace Application.Tests.Integration.Component.Contexts;

public class ShippingContextIntegrationTests
{
    private readonly ShippingContext _context;

    public ShippingContextIntegrationTests()
    {
        IShippingStrategyFactory factory = new ShippingStrategyFactory(Resolver);

        _context = new ShippingContext(factory);
        return;

        static IShippingStrategy Resolver(ShippingMethod method)
        {
            return method switch
            {
                ShippingMethod.Standard => new StandardShippingStrategy(),
                ShippingMethod.Express => new ExpressShippingStrategy(),
                _ => throw new NotSupportedException(
                    $"The shipping method '{method}' is not supported by the current test resolver in {nameof(ShippingContextIntegrationTests)}.")
            };
        }
    }

    [Theory]
    [InlineData(ShippingMethod.Standard, 10.0, 100.0, 60.0)]
    [InlineData(ShippingMethod.Express, 10.0, 100.0, 120.0)]
    public void ShouldCalculateShippingCostWhenStrategyIsSet(ShippingMethod method, double weight, double distance,
        double expectedCost)
    {
        // Arrange
        Order order = new(weight, distance, method);

        _context.SetStrategy(method);

        // Act
        double result = _context.CalculateShippingCost(order);

        // Assert
        Assert.Equal(expectedCost, result);
    }
}