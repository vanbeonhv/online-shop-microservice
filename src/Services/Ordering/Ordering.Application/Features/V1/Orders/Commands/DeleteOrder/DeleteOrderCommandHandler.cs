using MediatR;
using Ordering.Application.Common.Interfaces;
using Serilog;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly ILogger _logger;
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(ILogger logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }


    private const string METHOD_NAME = nameof(DeleteOrderCommandHandler);

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.Information("BEGIN: {MethodName} - OrderId: {OrderId}", METHOD_NAME, request.Id);

        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order == null)
        {
            _logger.Warning("Order with ID {OrderId} not found", request.Id);
            return;
        }

        await _orderRepository.DeleteAsync(order);
        await _orderRepository.SaveChangesAsync();

        _logger.Information("Deleted Order with Id: {OrderId}", request.Id);
        _logger.Information("END: {MethodName} - OrderId: {OrderId}", METHOD_NAME, request.Id);
    }
}