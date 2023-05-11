namespace eshopBackend.DAL.DTOs;

public record AddToCartDto
{
    public Guid ProductId { get; init; }
    public int Amount { get; init; }
}