using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;

namespace Ordering.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string userName)
    {
        var query = new GetOrderQuery(userName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{orderId:long}")]
    public async Task<ActionResult> DeleteOrder(long orderId)
    {
        var command = new DeleteOrderCommand(orderId);
        await _mediator.Send(command);
        return NoContent();
    }
}