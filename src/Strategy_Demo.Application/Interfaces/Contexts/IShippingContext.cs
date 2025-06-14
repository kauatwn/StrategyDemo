using Strategy_Demo.Domain.Entities;
using Strategy_Demo.Domain.Enums;

namespace Strategy_Demo.Application.Interfaces.Contexts;

public interface IShippingContext
{
    void SetStrategy(ShippingMethod method);
    double CalculateShippingCost(Order order);
}