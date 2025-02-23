using Domain.Enums;
using Domain.Interfaces.Strategies;

namespace Domain.Interfaces.Factories;

public interface IShippingStrategyFactory
{
    IShippingStrategy? Create(ShippingMethod method);
}