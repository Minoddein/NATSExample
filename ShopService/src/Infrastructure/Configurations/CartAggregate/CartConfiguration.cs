using Domain.CartAggregate.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.CartAggregate;

public class CartConfiguration: IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("carts");

        builder.HasKey(c => c.Id);

        builder.Ignore(c => c.TotalCount);

        builder.Property(c => c.UserId)
            .HasColumnName("user_id");
        
        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey("cart_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}