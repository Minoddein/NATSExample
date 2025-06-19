using Domain.Shared;

namespace Domain.ShopManagement.DomainEvents;

public record CategoryCreatedEvent(Guid CategoryId) : IDomainEvent;