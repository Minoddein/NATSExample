using Application.Abstractions;
using Application.Repositories;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Cart.Commands.CreateCart;

using Cart = Domain.CartAggregate.AggregateRoot.Cart;

public class CreateCartHandler: IRequestHandler<CreateCartCommand, Result>
{
    private readonly ILogger<CreateCartHandler> _logger;
    private readonly IValidator<CreateCartCommand> _validator;
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCartHandler(
        ILogger<CreateCartHandler> logger,
        IValidator<CreateCartCommand> validator, 
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _validator = validator;
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join("\n",  validationResult.Errors));
        }
        
        //TODO: Сделать проверку на существование корзины по user id
        
        var cartId = Guid.NewGuid();
        
        var cart = new Cart(cartId, request.UserId);

        await _cartRepository.CreateCart(cart, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Created cart {cartId}", cartId);
        
        return Result.Success();
    }
}