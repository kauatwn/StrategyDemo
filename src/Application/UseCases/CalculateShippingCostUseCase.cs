using Application.Interfaces.Contexts;
using Application.Interfaces.UseCases;
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