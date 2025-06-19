using Domain.Shared;

namespace Domain.ShopManagement.DomainEvents;

public record CategoryRemovedEvent(Guid CategoryId) : IDomainEvent;