using CSharpFunctionalExtensions;

namespace Domain.Shared;

public class DomainEntity: Entity<Guid>
{
    private readonly Queue<IDomainEvent> _events = [];

    public IReadOnlyList<IDomainEvent> Events => _events.ToList();
    
    public void AddEvent(IDomainEvent @event) =>  _events.Enqueue(@event);
    
    public void RemoveEvent() =>  _events.TryDequeue(out var @event);
    
    public void ClearEvents() => _events.Clear();
}