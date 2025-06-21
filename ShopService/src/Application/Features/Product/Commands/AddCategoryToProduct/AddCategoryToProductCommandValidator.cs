using FluentValidation;

namespace Application.Features.Product.Commands.AddCategoryToProduct;

public class AddCategoryToProductCommandValidator: AbstractValidator<AddCategoryToProductCommand>
{
    public AddCategoryToProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category Id is required");
        RuleFor(x => x.CatalogId).NotEmpty().WithMessage("Catalog Id is required");
    }
}