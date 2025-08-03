using MediatR;
using Ordering.Application.Common.Interfaces;
using Serilog;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrderById;

public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand>
{
    private const string METHOD_NAME = nameof(DeleteOrderByIdCommandHandler);
    private readonly ILogger _logger;
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderByIdCommandHandler(ILogger logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.Information("BEGIN: {MethodName} - OrderId: {OrderId}", METHOD_NAME, request.Id);

        var orderEntity = await _orderRepository.GetByIdAsync(request.Id);
        if (orderEntity == null)
        {
            _logger.Warning("Order with ID {OrderId} not found", request.Id);
            return;
        }

        await _orderRepository.DeleteAsync(orderEntity);
        orderEntity.DeletedOrder();
        await _orderRepository.SaveChangesAsync();


        _logger.Information("Deleted Order with Id: {OrderId}", request.Id);
        _logger.Information("END: {MethodName} - OrderId: {OrderId}", METHOD_NAME, request.Id);
    }
}