using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Features.Product.Commands.CreateCategory;

public record CreateCategoryCommand(Guid CatalogId, string Name): IRequest<Result<Guid>>;
