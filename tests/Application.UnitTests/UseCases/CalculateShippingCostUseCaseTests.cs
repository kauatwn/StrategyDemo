using Application.Abstractions.Contexts;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Application.UnitTests.UseCases;

public class CalculateShippingCostUseCaseTests
{
    private Mock<IShippingContext> MockContext { get; } = new();
    private CalculateShippingCostUseCase UseCase { get; }

    public CalculateShippingCostUseCaseTests()
    {
        UseCase = new CalculateShippingCostUseCase(MockContext.Object);
    }

    [Fact]
    public void ShouldCalculateShippingCost()
    {
        // Arrange
        var order = new Order(10, 100, ShippingMethod.Standard);

        MockContext.Setup(c => c.SetStrategy(It.IsAny<ShippingMethod>()));
        MockContext.Setup(c => c.CalculateShippingCost(It.IsAny<Order>()));

        // Act
        UseCase.Execute(order);

        // Assert
        MockContext.Verify(c => c.SetStrategy(It.IsAny<ShippingMethod>()), Times.Once);
        MockContext.Verify(c => c.CalculateShippingCost(It.IsAny<Order>()), Times.Once);
    }
}