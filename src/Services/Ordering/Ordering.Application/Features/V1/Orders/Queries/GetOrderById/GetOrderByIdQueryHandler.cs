using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler: IRequestHandler<GetOrderByIdQuery, ApiResult<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    private const string METHOD_NAME = nameof(GetOrderByIdQueryHandler);
    
    public async Task<ApiResult<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("$ BEGIN: {MethodName} - Id: {Id}", METHOD_NAME, request.Id);
        var orderEntity = await _orderRepository.GetByIdAsync(request.Id);
        var orderDto = _mapper.Map<OrderDto>(orderEntity);
        _logger.Information("$ END: {MethodName} - Id: {Id}", METHOD_NAME, request.Id);
        return new ApiSuccessResult<OrderDto>(orderDto);
    }
} 