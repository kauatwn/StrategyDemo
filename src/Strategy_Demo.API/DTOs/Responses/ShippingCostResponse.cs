namespace Strategy_Demo.API.DTOs.Responses;

public record ShippingCostResponse(string Message, double Cost, string ShippingMethod);