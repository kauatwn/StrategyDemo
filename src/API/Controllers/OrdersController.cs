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
    public IActionResult CalculateShippingCost(ICalculateShippingCostUseCase useCase, Order order)
    {
        ShippingCostResponse result = useCase.Execute(order);

        return Ok(result);
    }
}