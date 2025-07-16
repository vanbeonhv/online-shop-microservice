using AutoMapper;
using MediatR;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
{
    private const string METHOD_NAME = nameof(UpdateOrderCommandHandler);
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderCommandHandler(ILogger logger, IOrderRepository orderRepository, IMapper mapper)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _orderRepository.GetByIdAsync(request.Id);
        if (entity == null)
        {
            throw new NotFoundException($"Order with ID {request.Id} not found.");
        }

        _logger.Information("BEGIN: {MethodName} - OrderId: {OrderId}", METHOD_NAME, request.Id);
        _mapper.Map(request, entity);
        await _orderRepository.UpdateAsync(entity);
        await _orderRepository.SaveChangesAsync();
        _logger.Information("Updated Order with Id: {OrderId}", request.Id);
        _logger.Information("END: {MethodName} - OrderId: {OrderId}", METHOD_NAME, request.Id);

        var orderDto = _mapper.Map<OrderDto>(entity);
        return new ApiSuccessResult<OrderDto>(orderDto);
    }
}