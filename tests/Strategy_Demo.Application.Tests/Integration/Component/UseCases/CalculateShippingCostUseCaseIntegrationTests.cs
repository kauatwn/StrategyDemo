using Strategy_Demo.Application.Contexts;
using Strategy_Demo.Application.Interfaces.Contexts;
using Strategy_Demo.Application.UseCases;
using Strategy_Demo.Domain.Entities;
using Strategy_Demo.Domain.Enums;
using Strategy_Demo.Domain.Interfaces.Factories;
using Strategy_Demo.Domain.Interfaces.Strategies;
using Strategy_Demo.Infrastructure.Factories;
using Strategy_Demo.Infrastructure.Strategies.Shipping;

namespace Strategy_Demo.Application.Tests.Integration.Component.UseCases;

public class CalculateShippingCostUseCaseIntegrationTests
{
    private readonly CalculateShippingCostUseCase _useCase;

    public CalculateShippingCostUseCaseIntegrationTests()
    {
        IShippingStrategyFactory factory = new ShippingStrategyFactory(Resolver);
        IShippingContext context = new ShippingContext(factory);

        _useCase = new CalculateShippingCostUseCase(context);
        return;

        static IShippingStrategy Resolver(ShippingMethod method)
        {
            return method switch
            {
                ShippingMethod.Standard => new StandardShippingStrategy(),
                ShippingMethod.Express => new ExpressShippingStrategy(),
                _ => throw new NotSupportedException(
                    $"The shipping method '{method}' is not supported by the current test resolver in {nameof(CalculateShippingCostUseCaseIntegrationTests)}.")
            };
        }
    }

    [Theory]
    [InlineData(ShippingMethod.Standard, 10.0, 100.0, 60.0)]
    [InlineData(ShippingMethod.Express, 10.0, 100.0, 120.0)]
    public void ShouldCalculateShippingCost(ShippingMethod method, double weight, double distance, double expectedCost)
    {
        // Arrange
        Order order = new(weight, distance, method);

        // Act
        double result = _useCase.Execute(order);

        // Assert
        Assert.Equal(expectedCost, result);
    }
}