using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Features.Product.Commands.CreateProduct;

public record CreateProductCommand(
    Guid CatalogId,
    string Name,
    string Description,
    decimal Price,
    decimal Discount = 0,
    int Stock = 0) : IRequest<Result>;