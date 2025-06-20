using Domain.ProductAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ProductAggregate;

public class ProductConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(1000)
            .HasColumnName("name");
        
        builder.Property(c => c.Description)
            .HasMaxLength(10000)
            .HasColumnName("description");
        
        builder.Property(c => c.Price)
            .HasColumnName("price");
        
        builder.Property(c => c.Discount)
            .HasColumnName("discount");
        
        builder.Property(c => c.Stock)
            .HasColumnName("stock");
    }
}