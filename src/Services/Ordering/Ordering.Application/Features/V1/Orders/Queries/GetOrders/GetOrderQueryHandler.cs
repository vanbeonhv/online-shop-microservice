using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Shared.SeedWork;
using Serilog;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, ApiResult<List<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    private const string METHOD_NAME = nameof(GetOrderQueryHandler);

    public async Task<ApiResult<List<OrderDto>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("$ BEGIN: {MethodName} - User: {UserName}", METHOD_NAME, request.UserName);
        var orderEntities = await _orderRepository.GetOrdersByUserName(request.UserName);
        var orderDto = _mapper.Map<List<OrderDto>>(orderEntities);
        _logger.Information("$ END: {MethodName} - User: {UserName}", METHOD_NAME, request.UserName);
        return new ApiSuccessResult<List<OrderDto>>(orderDto);
    }
}