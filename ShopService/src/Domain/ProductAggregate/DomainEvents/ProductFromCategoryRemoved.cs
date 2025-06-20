using Domain.Shared;

namespace Domain.ProductAggregate.DomainEvents;

public record ProductFromCategoryRemoved(Guid CategoryId, Guid ProductId) : IDomainEvent;