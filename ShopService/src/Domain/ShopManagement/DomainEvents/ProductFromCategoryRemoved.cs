using Domain.Shared;

namespace Domain.ShopManagement.DomainEvents;

public record ProductFromCategoryRemoved(Guid CategoryId, Guid ProductId) : IDomainEvent;