using Application.Abstractions;
using Application.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<ShopDbContext>()
            .AddRepositories()
            .AddDatabase();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICatalogRepository, CatalogRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}