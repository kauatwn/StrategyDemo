using Application.Abstractions.Contexts;
using Application.Contexts;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Infrastructure.Factories;
using Infrastructure.Strategies.Shipping;

namespace Application.Tests.Integration.Component.UseCases;

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