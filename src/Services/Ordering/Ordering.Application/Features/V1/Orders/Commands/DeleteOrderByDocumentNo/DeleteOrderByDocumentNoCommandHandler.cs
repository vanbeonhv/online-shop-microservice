using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Features.V1.Orders.Commands.DeleteOrderById;
using Serilog;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrderByDocumentNo;

public class DeleteOrderByDocumentNoCommandHandler : IRequestHandler<DeleteOrderByDocumentNoCommand>
{
    private const string METHOD_NAME = nameof(DeleteOrderByDocumentNoCommandHandler);
    private readonly ILogger _logger;
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderByDocumentNoCommandHandler(ILogger logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task Handle(DeleteOrderByDocumentNoCommand request, CancellationToken cancellationToken)
    {
        // _logger.Information("BEGIN: {MethodName} - DocumentNo: {DocumentNo}", METHOD_NAME, request.DocumentNumber);
        //
        // var orderEntity = await _orderRepository.GetByIdAsync(request.DocumentNumber);
        // if (orderEntity == null)
        // {
        //     _logger.Warning("Order with DocumentNumber {DocumentNo} not found", request.DocumentNumber);
        //     return;
        // }
        //
        // await _orderRepository.DeleteAsync(orderEntity);
        // orderEntity.DeletedOrder();
        // await _orderRepository.SaveChangesAsync();
        //
        //
        // _logger.Information("Deleted Order with DocumentNo: {DocumentNo}", request.DocumentNumber);
        // _logger.Information("END: {MethodName} - DocumentNo: {DocumentNo}", METHOD_NAME, request.DocumentNumber);
    }
}