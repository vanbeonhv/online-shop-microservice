using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrdersByUserName;

public class GetOrderByUserNameQueryHandler : IRequestHandler<GetOrderByUserNameQuery, ApiResult<List<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetOrderByUserNameQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    private const string METHOD_NAME = nameof(GetOrderByUserNameQueryHandler);

    public async Task<ApiResult<List<OrderDto>>> Handle(GetOrderByUserNameQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("$ BEGIN: {MethodName} - User: {UserName}", METHOD_NAME, request.UserName);
        var orderEntities = await _orderRepository.GetOrdersByUserName(request.UserName);
        var orderDto = _mapper.Map<List<OrderDto>>(orderEntities);
        _logger.Information("$ END: {MethodName} - User: {UserName}", METHOD_NAME, request.UserName);
        return new ApiSuccessResult<List<OrderDto>>(orderDto);
    }
}