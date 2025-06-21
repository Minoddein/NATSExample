using Application.Abstractions;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.ProductAggregate.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Product.Commands.CreateCategory;

public class CreateCategoryHandler: IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly ILogger<CreateCategoryHandler> _logger;
    private readonly IValidator<CreateCategoryCommand> _validator;
    private readonly ICatalogRepository _catalogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(
        ILogger<CreateCategoryHandler> logger,
        IValidator<CreateCategoryCommand> validator,
        ICatalogRepository catalogRepository, 
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _validator = validator;
        _catalogRepository = catalogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure<Guid>(string.Join(",", validationResult.Errors));
        }

        var catalog = await _catalogRepository.GetCatalog(request.CatalogId, cancellationToken);

        if (catalog is null)
        {
            return Result.Failure<Guid>("Catalog not found");
        }

        var categoryId = Guid.NewGuid();

        var category = new Category(categoryId, request.Name);

        var result = catalog.AddCategory(category);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Category with id {categoryId} was created", categoryId);

        return Result.Success(categoryId);
    }
}