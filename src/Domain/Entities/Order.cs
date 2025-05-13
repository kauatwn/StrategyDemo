using Domain.Enums;

namespace Domain.Entities;

public class Order(double weight, double distance, ShippingMethod shippingMethod)
{
    public double Weight { get; } = weight;
    public double Distance { get; } = distance;
    public ShippingMethod ShippingMethod { get; } = shippingMethod;
}