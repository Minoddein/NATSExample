using Domain.Shared;

namespace Domain.ProductAggregate.DomainEvents;

public record CategoryCreatedEvent(Guid CategoryId) : IDomainEvent;