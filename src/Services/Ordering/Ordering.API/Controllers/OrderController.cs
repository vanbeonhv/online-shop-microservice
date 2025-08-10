using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.V1.Orders.Commands.CreateOrder;
using Ordering.Application.Features.V1.Orders.Commands.DeleteOrderById;
using Ordering.Application.Features.V1.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.V1.Orders.Queries.GetOrderById;
using Ordering.Application.Features.V1.Orders.Queries.GetOrdersByUserName;
using Shared.DTOs.Order;
using OrderDto = Ordering.Application.Common.Models.OrderDto;

namespace Ordering.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string userName)
    {
        var query = new GetOrderByUserNameQuery(userName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<OrderDto>> GetOrderById([Required] long id)
    {
        var query = new GetOrderByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<IEnumerable<OrderDto>>> CreateOrder([FromBody] CreateOrderDto model)
    {
        var query = _mapper.Map<CreateOrderCommand>(model);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPut("{id:long}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> UpdateOrder(long id, [FromBody] OrderDto model)
    {
        var query = _mapper.Map<UpdateOrderCommand>(model);
        query.Id = id;
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{orderId:long}")]
    public async Task<ActionResult> DeleteOrder(long orderId)
    {
        var command = new DeleteOrderByIdCommand(orderId);
        await _mediator.Send(command);
        return NoContent();
    }
}