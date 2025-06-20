using Domain.ProductAggregate.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ProductAggregate;

public class CatalogConfiguration: IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.ToTable("catalogs");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .HasMaxLength(300) //Избавиться от магических чисел
            .HasColumnType("name");
        
        builder.HasMany(c => c.Products)
            .WithOne()
            .HasForeignKey("catalog_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Categories)
            .WithOne()
            .HasForeignKey("catalog_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}