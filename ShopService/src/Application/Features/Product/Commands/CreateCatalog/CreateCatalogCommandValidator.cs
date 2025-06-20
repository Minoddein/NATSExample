using FluentValidation;

namespace Application.Features.Product.Commands.CreateCatalog;

public class CreateCatalogCommandValidator: AbstractValidator<CreateCatalogCommand>
{
    public CreateCatalogCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
    }
}