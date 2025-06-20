using Domain.Shared;

namespace Domain.ProductAggregate.DomainEvents;

public record ProductAtCategoryAdded(Guid CategoryId, Guid ProductId) : IDomainEvent;