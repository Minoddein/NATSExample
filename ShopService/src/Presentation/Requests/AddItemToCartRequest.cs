namespace Presentation.Requests;

public record AddItemToCartRequest(Guid UserId, Guid ProductId, int Quantity);
