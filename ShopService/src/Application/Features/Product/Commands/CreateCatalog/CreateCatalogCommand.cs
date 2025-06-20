using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Features.Product.Commands.CreateCatalog;

public record CreateCatalogCommand(string Name) : IRequest<Result>;
