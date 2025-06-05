using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name is required.")
            .MaximumLength(150).WithMessage("User name must not exceed 1 characters.");
    }
}