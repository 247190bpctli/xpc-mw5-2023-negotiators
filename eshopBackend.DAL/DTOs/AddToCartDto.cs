namespace eshopBackend.DAL.DTOs;

public record AddToCartDto
{
    public Guid CartId { get; init; }
    public Guid ProductId { get; init; }
    public int Amount { get; init; }
}
