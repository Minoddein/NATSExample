using Application.Abstractions;
using Application.Repositories;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Product.Commands.CreateProduct;

using Product = Domain.ProductAggregate.Entities.Product;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly ILogger<CreateProductHandler> _logger;
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly ICatalogRepository _catalogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(
        ILogger<CreateProductHandler> logger,
        IValidator<CreateProductCommand> validator,
        ICatalogRepository catalogRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _validator = validator;
        _catalogRepository = catalogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join(",", validationResult.Errors));
        }

        var catalog = await _catalogRepository.GetCatalog(request.CatalogId, cancellationToken);

        if (catalog is null)
        {
            return Result.Failure("Catalog not found");
        }

        var productId = Guid.NewGuid();

        var product = new Product(productId, request.Name, request.Description, request.Price, request.Stock);

        var result = catalog.AddProduct(product);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product {ProductId} was created", productId);

        return Result.Success();
    }
}