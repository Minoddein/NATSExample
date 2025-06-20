namespace Presentation.Requests;

public record CreateProductRequest(
    Guid CatalogId,
    string Name,
    string Description,
    decimal Price,
    decimal Discount = 0,
    int Stock = 0);