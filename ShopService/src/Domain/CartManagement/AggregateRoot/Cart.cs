using Domain.CartManagement.DomainEvents;
using Domain.CartManagement.Entities;
using Domain.Shared;

namespace Domain.CartManagement.AggregateRoot;

public sealed class Cart: DomainEntity
{
    private readonly List<CartItem> _items = [];

    public Cart(Guid id, Guid userId)
    {
        if (Guid.Empty.Equals(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (Guid.Empty.Equals(userId))
        {
            throw new ArgumentNullException(nameof(userId));
        }

        Id = id;
        UserId = userId;
    }

    //Мок, рандомный гуид, без привязки к AuthService и схемам Identity
    public Guid UserId { get; private set; }
    public IReadOnlyList<CartItem> Items => _items;
    public int TotalCount => _items.Count;

    public void AddItem(CartItem item)
    {
        var isExist = _items.FirstOrDefault(i => i.Id == item.Id);
        if (isExist is not null) 
        {
            isExist.Increase();
            return;
        }
        
        _items.Add(item);

        var @event = new ItemAddedEvent();
        
        AddEvent(@event);
    }

    public void RemoveItem(CartItem item)
    {
        var isExist = _items.FirstOrDefault(i => i.Id == item.Id);
        
        isExist?.Reduce();
    }

    public void ClearAllTheItems(CartItem item)
    {
        var isExist = _items.FirstOrDefault(i => i.Id == item.Id);
        if (isExist is not null)
        {
            _items.Remove(isExist);
        }
    }
}