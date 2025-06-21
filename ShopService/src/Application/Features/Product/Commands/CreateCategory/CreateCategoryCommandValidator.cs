using FluentValidation;

namespace Application.Features.Product.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.CatalogId).NotEmpty().WithMessage("Category Id cannot be empty");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}