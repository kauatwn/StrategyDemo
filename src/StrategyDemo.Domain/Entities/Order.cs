using StrategyDemo.Domain.Enums;

namespace StrategyDemo.Domain.Entities;

public sealed class Order(double weight, double distance, ShippingMethod shippingMethod)
{
    public double Weight { get; } = weight;
    public double Distance { get; } = distance;
    public ShippingMethod ShippingMethod { get; } = shippingMethod;
}