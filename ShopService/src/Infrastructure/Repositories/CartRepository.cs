using Application.Repositories;
using Domain.CartAggregate.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartRepository(ShopDbContext context): ICartRepository
{
    public async Task<Guid> CreateCart(Cart cart, CancellationToken cancellation = default)
    {
        await context.Carts.AddAsync(cart, cancellation);
        
        return cart.Id;
    }

    public async Task<Cart?> GetCart(Guid id, CancellationToken cancellation = default)
    {
        var cart =  await context.Carts.FirstOrDefaultAsync(c => c.Id == id, cancellation);
        
        return cart;
    }
}