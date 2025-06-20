﻿using CSharpFunctionalExtensions;
using Domain.ProductAggregate.DomainEvents;
using Domain.ProductAggregate.Entities;
using Domain.Shared;

namespace Domain.ProductAggregate.AggregateRoot;

public class Catalog: DomainEntity
{
    private readonly List<Category> _categories = [];
    private readonly List<Product> _products = [];
    
    // Ef core
    private Catalog(){}
    
    public Catalog(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }
        
        Id = id;
        Name = name;
    }
    
    public string Name { get; private set; }
    public IReadOnlyList<Category> Categories => _categories;
    public IReadOnlyList<Product> Products => _products;

    public Result AddCategoryToProduct(Guid productId, Guid categoryId)
    {
        var category = _categories.FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            return Result.Failure("Category not found");
        }
        
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            return Result.Failure("Product not found");
        }
        
        category.AddProduct(productId);
        product.AddCategory(categoryId);
        
        return Result.Success();
    }
    
    public Result AddCategory(Category category)
    {
        var isExist =  _categories.Any(c => c.Name == category.Name);
        if (isExist)
        {
            return Result.Failure("Category already exists");
        }
        
        _categories.Add(category);

        var @event = new CategoryCreatedEvent(category.Id);

        AddEvent(@event);
        
        return Result.Success();
    }
    
    public Result AddProduct(Product product)
    {
        var isExistProduct = _products.FirstOrDefault(p => p.Name == product.Name);

        if (isExistProduct is not null)
        {
            isExistProduct.AddQuantity(product.Stock);
            return Result.Failure("Product already exists");
        }
        
        _products.Add(product);

        var @event = new ProductCreatedEvent(product.Id);

        AddEvent(@event);
        
        return Result.Success();
    }

    public void ReduceProduct(Product product, int quantity)
    {
        var isExistProduct = _products.FirstOrDefault(p => p.Id == product.Id);

        isExistProduct?.Reduce(quantity);
    }

    public Result RemoveCategory(Category category)
    {
        var isExist = _categories.Any(c => c.Id == category.Id);
        if (!isExist)
        {
            return Result.Failure("Category not found");
        }
        
        _categories.Remove(category);
        
        var @event = new CategoryRemovedEvent(category.Id);
        
        AddEvent(@event);
        
        return Result.Success();
    }
    
    public Result RemoveProduct(Product product)
    {
        var isExist = _products.Any(c => c.Id == product.Id);
        if (!isExist)
        {
            return Result.Failure("Product not found");
        }
        
        _products.Remove(product);

        var categories = _categories.Where(c => c.Products.Contains(product.Id)).ToList();
        
        categories.ForEach(c => c.RemoveProduct(product.Id));
        
        var @event = new ProductRemovedEvent(product.Id);
        
        AddEvent(@event);
        
        return Result.Success();
    }
}