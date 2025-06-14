using Strategy_Demo.Domain.Enums;

namespace Strategy_Demo.Domain.Entities;

public class Order(double weight, double distance, ShippingMethod shippingMethod)
{
    public double Weight { get; } = weight;
    public double Distance { get; } = distance;
    public ShippingMethod ShippingMethod { get; } = shippingMethod;
}