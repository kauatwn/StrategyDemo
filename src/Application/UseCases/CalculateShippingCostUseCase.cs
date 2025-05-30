using Application.Abstractions.Contexts;
using Application.Abstractions.UseCases;
using Domain.Entities;

namespace Application.UseCases;

public class CalculateShippingCostUseCase(IShippingContext context) : ICalculateShippingCostUseCase
{
    public double Execute(Order order)
    {
        context.SetStrategy(order.ShippingMethod);
        return context.CalculateShippingCost(order);
    }
}