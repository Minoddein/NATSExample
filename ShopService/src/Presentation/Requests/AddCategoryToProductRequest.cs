namespace Presentation.Requests;

public record AddCategoryToProductRequest(Guid CatalogId,Guid CategoryId, Guid ProductId);
