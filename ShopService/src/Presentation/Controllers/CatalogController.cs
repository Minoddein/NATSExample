using Application.Features.Cart.Commands.CreateCart;
using Application.Features.Product.Commands.CreateCatalog;
using Application.Features.Product.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

public class CatalogController : ApplicationController
{
    [HttpPost("creation-catalog")]
    public async Task<IActionResult> CreateCatalog(
        [FromBody] CreateCatalogRequest request,
        [FromServices] CreateCatalogHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCatalogCommand(request.Name);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.IsSuccess);
    }

    [HttpPost("creation-product")]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        [FromServices] CreateProductHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateProductCommand(
            request.CatalogId, 
            request.Name, 
            request.Description, 
            request.Price,
            request.Discount,
            request.Stock);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.IsSuccess);
    }
}