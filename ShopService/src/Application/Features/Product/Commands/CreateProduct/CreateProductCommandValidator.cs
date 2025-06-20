using FluentValidation;

namespace Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount must be greater or equal to 0");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock must be greater or equal to 0");
    }
}