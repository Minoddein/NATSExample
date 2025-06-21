using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Features.Cart.Commands.AddItemToCart;

public record AddItemToCartCommand(Guid UserId, Guid ProductId, int Quantity) :  IRequest<Result>;
