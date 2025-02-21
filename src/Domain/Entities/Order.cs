using Domain.Enums;

namespace Domain.Entities;

public class Order(double weight, double distance, ShippingMethod shippingMethod)
{
    public double Weight { get; set; } = weight;
    public double Distance { get; set; } = distance;
    public ShippingMethod ShippingMethod { get; set; } = shippingMethod;
}