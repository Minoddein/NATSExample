using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Features.Product.Commands.AddCategoryToProduct;

public record AddCategoryToProductCommand(Guid CatalogId, Guid ProductId, Guid CategoryId) : IRequest<Result>;
