using Application.Repositories;
using Domain.ProductAggregate.AggregateRoot;
using Domain.ProductAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CatalogRepository(ShopDbContext context) : ICatalogRepository
{
    public async Task<Guid> CreateCatalog(Catalog catalog, CancellationToken cancellation = default)
    {
        await context.Catalogs.AddAsync(catalog, cancellation);

        return catalog.Id;
    }

    public async Task<Catalog?> GetCatalog(Guid id, CancellationToken cancellation = default)
    {
        var catalog = await context.Catalogs.FirstOrDefaultAsync(c => c.Id == id, cancellation);

        return catalog;
    }

    public async Task<List<Product>> GetProductsWithPagination(
        Guid id,
        int pageNumber,
        int pageSize,
        CancellationToken cancellation = default)
    {
        return await context.Catalogs
            .Where(c => c.Id == id)
            .SelectMany(c => c.Products) 
            .OrderBy(p => p.Id)         
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()             
            .ToListAsync(cancellation);
    }

    public async Task<List<Category>> GetCategories(
        Guid id,
        int pageNumber,
        int pageSize,
        CancellationToken cancellation = default)
    {
        return await context.Catalogs
            .Where(c => c.Id == id)
            .SelectMany(c => c.Categories) 
            .OrderBy(c => c.Id)           
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()               
            .ToListAsync(cancellation);
    }

    public async Task<List<Product>> GetProductsByCategories(Guid id,
        IEnumerable<Guid> categoryIds,
        int pageNumber,
        int pageSize,
        CancellationToken cancellation = default)
    {
        var categoryIdsList = categoryIds.ToList();

        var query = context.Catalogs
            .Where(c => c.Id == id)
            .SelectMany(c => c.Products
                .Where(p => p.Categories.Any(categoryId => categoryIdsList.Contains(categoryId)))
                .OrderBy(p => p.Id) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize))
            .AsNoTracking();

        return await query.ToListAsync(cancellation);
    }
}