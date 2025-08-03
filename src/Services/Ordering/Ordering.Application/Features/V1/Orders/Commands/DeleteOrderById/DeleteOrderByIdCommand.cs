using MediatR;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrderById;

public class DeleteOrderByIdCommand: IRequest
{
    public long Id { get; private set;  }

    public DeleteOrderByIdCommand(long id)
    {
        Id = id;
    }
    
}