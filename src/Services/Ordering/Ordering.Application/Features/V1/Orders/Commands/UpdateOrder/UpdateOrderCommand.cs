using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Common;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand: CreateOrUpdateCommand, IRequest<ApiResult<OrderDto>>
{
    public long Id { get; set; }
}