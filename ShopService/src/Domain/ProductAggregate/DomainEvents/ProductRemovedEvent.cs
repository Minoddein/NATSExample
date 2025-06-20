using Domain.Shared;

namespace Domain.ProductAggregate.DomainEvents;

public record ProductRemovedEvent(Guid ProductId) : IDomainEvent;