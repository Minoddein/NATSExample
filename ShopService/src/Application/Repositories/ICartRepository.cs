using Domain.CartAggregate.AggregateRoot;

namespace Application.Repositories;



public interface ICartRepository
{
    Task<Guid> CreateCart(Domain.CartAggregate.AggregateRoot.Cart cart, CancellationToken cancellation = default);
    Task<Domain.CartAggregate.AggregateRoot.Cart?> GetCart(Guid id, CancellationToken cancellation = default);
}