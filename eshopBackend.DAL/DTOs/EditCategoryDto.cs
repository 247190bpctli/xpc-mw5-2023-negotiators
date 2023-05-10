namespace eshopBackend.DAL.DTOs;

public record EditCategoryDto
{
    public string Name { get; init; }
    public string ImageUrl { get; init; }
    public string Description { get; init; }
}
