using Application.Abstractions;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.CartAggregate.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Cart.Commands.AddItemToCart;

public class AddItemToCartHandler: IRequestHandler<AddItemToCartCommand, Result>
{
    private readonly ILogger<AddItemToCartHandler> _logger;
    private readonly IValidator<AddItemToCartCommand> _validator;
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddItemToCartHandler(
        ILogger<AddItemToCartHandler> logger,
        IValidator<AddItemToCartCommand> validator, 
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _validator = validator;
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var  validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join("\n",  validationResult.Errors));
        }
        
        var cart = await _cartRepository.GetCart(request.UserId, cancellationToken);
        if (cart is null)
        {
            return Result.Failure("Cart not found");
        }

        var cartId = Guid.NewGuid();
        
        var cartItem = new CartItem(cartId, request.ProductId, request.Quantity, _dateTimeProvider.UtcNow);
        
        cart.AddItem(cartItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Added product {productId} to cart {cartId}",  request.ProductId, cartId);
        
        return Result.Success();
    }
}