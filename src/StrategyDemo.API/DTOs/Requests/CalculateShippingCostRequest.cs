using StrategyDemo.Domain.Enums;

namespace StrategyDemo.API.DTOs.Requests;

public sealed record CalculateShippingCostRequest(double Weight, double Distance, ShippingMethod ShippingMethod);