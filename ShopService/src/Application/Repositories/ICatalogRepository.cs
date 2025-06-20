using Domain.ProductAggregate.AggregateRoot;
using Domain.ProductAggregate.Entities;

namespace Application.Repositories;

public interface ICatalogRepository
{
    Task<Guid> CreateCatalog(Catalog catalog, CancellationToken cancellation = default);
    Task<Catalog?> GetCatalog(Guid id, CancellationToken cancellation = default);

    Task<List<Product>> GetProductsWithPagination(Guid id, int pageNumber, int pageSize,
        CancellationToken cancellation = default);

    Task<List<Category>> GetCategories(Guid id, int pageNumber, int pageSize, CancellationToken cancellation = default);
    
    Task Attach(Catalog catalog, CancellationToken cancellation = default);

    Task<List<Product>> GetProductsByCategories(
        Guid id,
        IEnumerable<Guid> categoryIds,
        int pageNumber,
        int pageSize,
        CancellationToken cancellation = default);
}