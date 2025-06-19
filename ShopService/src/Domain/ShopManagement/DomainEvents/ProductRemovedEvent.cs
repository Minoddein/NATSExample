using Domain.Shared;

namespace Domain.ShopManagement.DomainEvents;

public record ProductRemovedEvent(Guid ProductId) : IDomainEvent;