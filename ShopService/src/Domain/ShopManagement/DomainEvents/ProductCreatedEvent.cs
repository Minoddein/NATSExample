using Domain.Shared;

namespace Domain.ShopManagement.DomainEvents;

public record ProductCreatedEvent(Guid ProductId) : IDomainEvent;