using Application.Abstractions.Contexts;
using Application.Abstractions.UseCases;
using Application.DTOs;
using Domain.Entities;

namespace Application.UseCases;

public class CalculateShippingCostUseCase(IShippingContext context) : ICalculateShippingCostUseCase
{
    private IShippingContext Context { get; } = context;

    public ShippingCostResponse Execute(Order order)
    {
        Context.SetStrategy(order.ShippingMethod);

        double cost = Context.CalculateShippingCost(order);
        var message = $"The {order.ShippingMethod.ToString().ToLower()} shipping cost is {cost:C}";

        return new ShippingCostResponse(message, cost, order.ShippingMethod.ToString());
    }
}