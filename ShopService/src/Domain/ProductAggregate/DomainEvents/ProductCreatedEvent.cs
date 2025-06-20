using Domain.Shared;

namespace Domain.ProductAggregate.DomainEvents;

public record ProductCreatedEvent(Guid ProductId) : IDomainEvent;