using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Features.Cart.Commands.CreateCart;

public record CreateCartCommand(Guid UserId): IRequest<Result>;