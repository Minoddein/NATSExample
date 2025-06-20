using Domain.Shared;

namespace Domain.ProductAggregate.DomainEvents;

public record CategoryRemovedEvent(Guid CategoryId) : IDomainEvent;