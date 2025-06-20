using Application.Abstractions;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.ProductAggregate.AggregateRoot;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Product.Commands.CreateCatalog;

public class CreateCatalogHandler: IRequestHandler<CreateCatalogCommand, Result>
{
    private readonly ILogger<CreateCatalogHandler> _logger;
    private readonly IValidator<CreateCatalogCommand> _validator;
    private readonly ICatalogRepository _catalogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCatalogHandler(
        ILogger<CreateCatalogHandler> logger,
        IValidator<CreateCatalogCommand> validator,
        ICatalogRepository catalogRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _validator = validator;
        _catalogRepository = catalogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure(string.Join(",",  validationResult.Errors));
        }
        
        var catalogId = Guid.NewGuid();
        
        var catalog = new Catalog(catalogId, request.Name);

        await _catalogRepository.CreateCatalog(catalog, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Created catalog {catalogId}", catalogId);
        
        return Result.Success();
    }
}