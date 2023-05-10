namespace eshopBackend.DAL.DTOs;

public record EditCategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string ImageUrl { get; init; }
    public string Description { get; init; }
}
