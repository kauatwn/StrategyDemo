using Microsoft.AspNetCore.Mvc.Testing;
using StrategyDemo.API.DTOs.Requests;
using StrategyDemo.API.DTOs.Responses;
using StrategyDemo.Domain.Enums;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrategyDemo.API.Tests.Functional;

public class OrdersControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    [Theory]
    [InlineData(10.0, 100.0, ShippingMethod.Standard, 60.0)]
    [InlineData(10.0, 100.0, ShippingMethod.Express, 120.0)]
    public async Task ShouldReturnCorrectShippingCost(
        double weight, double distance, ShippingMethod shippingMethod, double expectedCost)
    {
        // Arrange
        HttpClient client = factory.CreateClient();
        CalculateShippingCostRequest request = new(weight, distance, shippingMethod);

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/Orders/CalculateShippingCost", request, _jsonOptions);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        string content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ShippingCostResponse>(content, _jsonOptions);

        Assert.NotNull(result);
        Assert.Equal(expectedCost, result.Cost);
        Assert.Equal(shippingMethod.ToString(), result.ShippingMethod);
        Assert.Contains(shippingMethod.ToString().ToLower(), result.Message.ToLower());
    }

    [Theory]
    [InlineData("Invalid")]
    [InlineData("")]
    public async Task ShouldReturnBadRequestWhenShippingMethodIsInvalid(string invalidMethod)
    {
        // Arrange
        HttpClient client = factory.CreateClient();
        var request = new { weight = 10.0, distance = 100.0, shippingMethod = invalidMethod };

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/Orders/CalculateShippingCost", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}