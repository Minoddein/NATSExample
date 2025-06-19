using Domain.Shared;

namespace Domain.ShopManagement.DomainEvents;

public record ProductAtCategoryAdded(Guid CategoryId, Guid ProductId) : IDomainEvent;