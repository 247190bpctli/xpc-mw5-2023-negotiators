namespace eshopBackend.DAL.DTOs;

public record ManufacturerDto
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string LogoUrl { get; init; } = null!;
    public string Origin { get; init; } = null!;
}