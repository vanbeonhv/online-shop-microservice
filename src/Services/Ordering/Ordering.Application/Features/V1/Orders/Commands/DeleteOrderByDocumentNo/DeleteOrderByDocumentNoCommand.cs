using MediatR;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrderByDocumentNo;

public class DeleteOrderByDocumentNoCommand: IRequest
{
    public string DocumentNumber { get; private set;  }

    public DeleteOrderByDocumentNoCommand(string documentNumber)
    {
        DocumentNumber = documentNumber;
    }
    
}