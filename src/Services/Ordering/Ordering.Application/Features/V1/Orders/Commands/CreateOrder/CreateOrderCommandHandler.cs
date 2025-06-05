using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, ApiResult<long>>
{
    private readonly ILogger _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(ILogger logger, IOrderRepository orderRepository, IMapper mapper)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    private const string METHOD_NAME = nameof(CreateOrderCommandHandler);

    public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.Information("$ BEGIN: {MethodName} - User: {UserName}", METHOD_NAME, request.UserName);
        var order = _mapper.Map<Order>(request);

        var newOrderId = await _orderRepository.CreateAsync(order);
        await _orderRepository.SaveChangesAsync();
        _logger.Information("$ Created Order with Id: {OrderId}", newOrderId);
        _logger.Information("$ END: {MethodName} - User: {UserName}", METHOD_NAME, request.UserName);

        return new ApiSuccessResult<long>(newOrderId);
    }
}