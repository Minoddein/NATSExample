using FluentValidation;

namespace Application.Features.Cart.Commands.CreateCart;

public class CreateCartCommandValidator: AbstractValidator<CreateCartCommand>
{
    public CreateCartCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithErrorCode("user is required");
    }
}