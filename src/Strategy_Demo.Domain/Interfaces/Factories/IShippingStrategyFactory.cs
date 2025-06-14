using Strategy_Demo.Domain.Enums;
using Strategy_Demo.Domain.Interfaces.Strategies;

namespace Strategy_Demo.Domain.Interfaces.Factories;

public interface IShippingStrategyFactory
{
    IShippingStrategy Create(ShippingMethod method);
}