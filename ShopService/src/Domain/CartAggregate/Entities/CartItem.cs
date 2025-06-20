using CSharpFunctionalExtensions;

namespace Domain.CartAggregate.Entities;

public sealed class CartItem : Entity<Guid>
{
    public CartItem(Guid id, Guid productId, int quantity, DateTime dateCreated)
    {
        #region constraints
        if (Guid.Empty == productId)
        {
            throw new ArgumentException("ProductId cannot be empty");
        }

        if (Guid.Empty == id)
        {
            throw new ArgumentException("Id cannot be empty");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity cannot be zero or negative");
        }

        if (dateCreated > DateTime.Now)
        {
            throw new ArgumentException("Date cannot be in the future");
        }
        #endregion
        
        Id = id;
        ProductId = productId;
        Quantity = quantity;
        DateCreated = dateCreated;
    }
    
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateCreated { get; private set; }

    public void Reduce()
    {
        if (Quantity > 0)
        {
            Quantity--;
        }
    }

    public void Increase()
    {
        Quantity++;
    }
    
}