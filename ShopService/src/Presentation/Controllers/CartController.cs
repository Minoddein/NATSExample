using Application.Features.Cart.Commands.CreateCart;
using Microsoft.AspNetCore.Mvc;

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
}