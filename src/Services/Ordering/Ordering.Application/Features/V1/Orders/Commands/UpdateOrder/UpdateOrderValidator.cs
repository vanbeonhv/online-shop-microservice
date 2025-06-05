using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Order ID is required.");
    }
}