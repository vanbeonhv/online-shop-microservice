using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrdersByUserName;

public class GetOrderByUserNameQuery: IRequest<ApiResult<List<OrderDto>>>
{
    public string UserName { get; private set; }

    public GetOrderByUserNameQuery(string userName)
    {
        UserName = userName;
    }
}