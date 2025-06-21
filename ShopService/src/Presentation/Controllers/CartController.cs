using Application.Features.Cart.Commands.AddItemToCart;
using Application.Features.Cart.Commands.CreateCart;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

public class CartController: ApplicationController
{
    [HttpPost("creation-cart")]
    public async Task<IActionResult> CreateCart(
        [FromBody] Guid userId,
        [FromServices] CreateCartHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCartCommand(userId);
        
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.IsSuccess);
    }
    
    [HttpPost("item-to-cart")]
    public async Task<IActionResult> AddItemToCart(
        [FromBody] AddItemToCartRequest request,
        [FromServices] AddItemToCartHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new AddItemToCartCommand(request.UserId, request.ProductId, request.Quantity);
        
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.IsSuccess);
    }
}