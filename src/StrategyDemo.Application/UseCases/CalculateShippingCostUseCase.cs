using StrategyDemo.Domain.Entities;
using StrategyDemo.Application.Interfaces.Contexts;
using StrategyDemo.Application.Interfaces.UseCases;

namespace StrategyDemo.Application.UseCases;

public sealed class CalculateShippingCostUseCase(IShippingContext context) : ICalculateShippingCostUseCase
{
    public double Execute(Order order)
    {
        context.SetStrategy(order.ShippingMethod);
        return context.CalculateShippingCost(order);
    }
}