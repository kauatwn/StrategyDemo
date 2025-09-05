namespace StrategyDemo.API.DTOs.Responses;

public sealed record ShippingCostResponse(string Message, double Cost, string ShippingMethod);