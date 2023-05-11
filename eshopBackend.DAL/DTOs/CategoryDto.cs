namespace eshopBackend.DAL.DTOs;

public record CategoryDto
{
    public string Name { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public string Description { get; init; } = null!;
}