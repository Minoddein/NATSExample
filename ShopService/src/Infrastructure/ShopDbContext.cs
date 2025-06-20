using Domain.CartManagement.AggregateRoot;
using Domain.ShopManagement.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class ShopDbContext(IConfiguration configuration): DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .UseLoggerFactory(CreateLoggerFactory)
            .EnableSensitiveDataLogging()
            .UseSnakeCaseNamingConvention();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("shop");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ShopDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations") ?? false);
    }
    
    private static readonly ILoggerFactory CreateLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    public DbSet<Catalog> Catalogs { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
}