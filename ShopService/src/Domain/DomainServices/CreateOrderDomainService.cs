using CSharpFunctionalExtensions;
using Domain.CartManagement.AggregateRoot;
using Domain.ShopManagement.AggregateRoot;

namespace Domain.DomainServices;

public class CreateOrderDomainService
{
    public Result CreateOrder(Cart cart, Catalog catalog)
    {   
        

        return Result.Success();
    }
} 