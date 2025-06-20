using CSharpFunctionalExtensions;
using Domain.CartManagement.Aggregate;
using Domain.ShopManagement.Aggregate;

namespace Domain.DomainServices;

public class CreateOrderDomainService
{
    public Result CreateOrder(Cart cart, Catalog catalog)
    {   
        

        return Result.Success();
    }
} 