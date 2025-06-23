using StrategyDemo.Application.Contexts;
using StrategyDemo.Domain.Entities;
using StrategyDemo.Domain.Enums;
using StrategyDemo.Domain.Interfaces.Factories;
using StrategyDemo.Domain.Interfaces.Strategies;
using StrategyDemo.Infrastructure.Factories;
using StrategyDemo.Infrastructure.Strategies.Shipping;

namespace StrategyDemo.Application.Tests.Integration.Contexts;

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