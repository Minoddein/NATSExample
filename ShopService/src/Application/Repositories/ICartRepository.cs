using Domain.CartAggregate.AggregateRoot;

namespace Application.Repositories;

public interface ICartRepository
{
    Task<Guid> CreateCart(Cart cart, CancellationToken cancellation = default);
    Task<Cart?> GetCart(Guid id, CancellationToken cancellation = default);
}