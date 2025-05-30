using API.DTOs.Responses;
using Application.Abstractions.UseCases;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpPost("CalculateShippingCost")]
    [ProducesResponseType<ShippingCostResponse>(StatusCodes.Status200OK)]
    public ActionResult<ShippingCostResponse> CalculateShippingCost(ICalculateShippingCostUseCase useCase, Order order)
    {
        ShippingCostResult result = useCase.Execute(order);
        ShippingCostResponse response = new($"The {result.ShippingMethod.ToLower()} shipping cost is {result.Cost:C}",
            result.Cost, result.ShippingMethod);

        return Ok(response);
    }
}