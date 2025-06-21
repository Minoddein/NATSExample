using Application.Abstractions;
using Application.Repositories;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Product.Commands.AddCategoryToProduct;

public class AddCategoryToProductHandler : IRequestHandler<AddCategoryToProductCommand, Result>
{
    private readonly ILogger<AddCategoryToProductHandler> _logger;
    private readonly IValidator<AddCategoryToProductCommand> _validator;
    private readonly ICatalogRepository _catalogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCategoryToProductHandler(
        ILogger<AddCategoryToProductHandler> logger,
        IValidator<AddCategoryToProductCommand> validator,
        ICatalogRepository catalogRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _validator = validator;
        _catalogRepository = catalogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddCategoryToProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join(",", validationResult.Errors));
        }

        var catalog = await _catalogRepository.GetCatalog(request.CatalogId, cancellationToken);
        if (catalog == null)
        {
            return Result.Failure("Catalog not found");
        }

        var result = catalog.AddCategoryToProduct(request.ProductId, request.CategoryId);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Added category {categoryId} to product {productId}",
            request.CategoryId,
            request.ProductId);

        return Result.Success();
    }
}