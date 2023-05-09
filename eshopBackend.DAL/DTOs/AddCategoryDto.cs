namespace eshopBackend.DAL.DTOs;

public record AddCategoryDto
{
    public string Name { get; init; }
    public string? ImageUrl { get; init; }
    public string? Description { get; init; }
}
