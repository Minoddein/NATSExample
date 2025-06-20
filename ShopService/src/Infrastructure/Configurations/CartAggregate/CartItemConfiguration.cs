using Domain.CartManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.CartAggregate;

public class CartItemConfiguration: IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("cart_items");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.DateCreated)
            .HasColumnName("date_created");
        
        builder.Property(c => c.ProductId)
            .HasColumnName("product_id");
        
        builder.Property(c => c.Quantity)
            .HasColumnName("quantity");
    }
}