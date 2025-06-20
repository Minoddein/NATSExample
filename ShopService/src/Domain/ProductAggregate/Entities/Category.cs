using Domain.ProductAggregate.DomainEvents;
using Domain.Shared;

namespace Domain.ProductAggregate.Entities;

public sealed class Category: DomainEntity
{
    private readonly List<Guid> _products = [];

    public Category(Guid id, string name)
    {
        if (Guid.Empty.Equals(id))
        {
            throw new ArgumentException(null, nameof(id));
        }
        
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }
        
        Id = id;
        Name = name;
    }
    
    public string Name { get; private set; }
    public IReadOnlyList<Guid> Products => _products;

    public void AddProduct(Guid productId)
    {
        _products.Add(productId);
        
        var @event = new ProductAtCategoryAdded(Id, productId);
        
        AddEvent(@event);
    }

    public void RemoveProduct(Guid productId)
    {
        _products.Remove(productId);
        
        var @event = new ProductFromCategoryRemoved(Id, productId);
        
        AddEvent(@event);
    }
}