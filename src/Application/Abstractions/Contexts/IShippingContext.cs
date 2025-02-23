using Domain.Entities;
using Domain.Enums;

namespace Application.Abstractions.Contexts;

public interface IShippingContext
{
    void SetStrategy(ShippingMethod method);
    double CalculateShippingCost(Order order);
}