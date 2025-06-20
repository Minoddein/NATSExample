using CSharpFunctionalExtensions;
using Domain.CartAggregate.AggregateRoot;
using Domain.ProductAggregate.AggregateRoot;

namespace Domain.DomainServices;

public class CreateOrderDomainService
{
    public Result CreateOrder(Cart cart, Catalog catalog)
    {   
        

        return Result.Success();
    }
} 