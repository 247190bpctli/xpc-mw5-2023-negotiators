namespace eshopBackend.DAL.DTOs;

public record ProductDto
{
    public string Name { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public string Description { get; init; } = null!;
    public double Price { get; init; }
    public double Weight { get; init; }
    public int Stock { get; init; }
    public Guid CategoryId { get; init; }
    public Guid ManufacturerId { get; init; }
}