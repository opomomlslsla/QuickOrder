using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using QuickOrder.Domain.Interfaces;
using QuickOrder.Domain.Models;
using QuickOrder.Server.DTO;

namespace ReactApp1.Server.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] int page, CancellationToken cancellationToken)
    {
        var orders = await orderService.GetOrdersAsync(page, cancellationToken);
        var result = orders.Select(x => new OrderBaseInfo(x.Id, x.SenderCity, x.RecipientCity, x.PickupDate, x.Status, x.SerialNumber)).ToArray();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderByIdAsync(id, cancellationToken);

        if (order is null)
            return NotFound($"Order with ID {id} not found");

        var result = new OrderFullInfo(
            order.Id,
            order.SenderCity,
            order.SenderAddress,
            order.RecipientCity,
            order.RecipientAddress,
            order.Weight,
            order.PickupDate,
            order.Status,
            order.SerialNumber);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        if(request.Weight < 0 ||
            request.SenderCity.Trim().IsNullOrEmpty() ||
            request.RecipientCity.Trim().IsNullOrEmpty() ||
            request.SenderAddress.Trim().IsNullOrEmpty() ||
            request.RecipientAddress.Trim().IsNullOrEmpty() ||
            request.PickupDate < DateTime.UtcNow.Date)
        {
            return BadRequest("Not valid request data");
        }

        var order = new Order
        {
            SenderCity = request.SenderCity,
            SenderAddress = request.SenderAddress,
            RecipientCity = request.RecipientCity,
            RecipientAddress = request.RecipientAddress,
            Weight = request.Weight,
            PickupDate = request.PickupDate,
        };

        await orderService.AddOrderAsync(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }
}
