using CSharpFunctionalExtensions;

namespace Domain.ProductAggregate.Entities;

// Упрощаем до того, что у продукта нет производителя, продавца
// И создание нового такого же продукта, проверяется по имени и суммируется на складе соответственно
public sealed class Product : Entity<Guid>
{
    private readonly List<Guid> _categories = [];

    public Product(Guid id, string name, string description, decimal price, int stock, decimal discount = 0)
    {
        #region constraints

        if (Guid.Empty.Equals(id))
        {
            throw new ArgumentException(null, nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(null, nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(null, nameof(description));
        }

        if (price <= 0)
        {
            throw new ArgumentException(null, nameof(price));
        }

        if (stock < 0)
        {
            throw new ArgumentException(null, nameof(stock));
        }

        if (discount < 0)
        {
            throw new ArgumentException(null, nameof(discount));
        }

        #endregion

        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Discount = discount;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public decimal Discount { get; private set; }
    public int Stock { get; private set; }
    public IReadOnlyList<Guid> Categories => _categories;

    public void AddQuantity(int quantity)
    {
        Stock += quantity;
    }

    public void Reduce(int quantity)
    {
        Stock -= quantity;
    }
}