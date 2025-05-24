using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders;

public class GetOrderQuery: IRequest<ApiResult<List<OrderDto>>>
{
    public string UserName { get; private set; }

    public GetOrderQuery(string userName)
    {
        UserName = userName;
    }
}