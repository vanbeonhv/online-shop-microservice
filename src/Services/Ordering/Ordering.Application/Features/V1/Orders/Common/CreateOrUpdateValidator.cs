using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Common;

public class CreateOrUpdateValidator : AbstractValidator<CreateOrUpdateCommand>
{
    public CreateOrUpdateValidator()
    {
        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("Total price must be greater than zero.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(250).WithMessage("Last name must not exceed 250 characters.");

        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address is required.")
            .MaximumLength(250).WithMessage("Shipping address must not exceed 250 characters.");

        RuleFor(x => x.InvoiceAddress)
            .NotEmpty().WithMessage("Invoice address is required.");
    }
}