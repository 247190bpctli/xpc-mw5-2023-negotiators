namespace eshopBackend.DAL.DTOs;

public record ManufacturerDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string LogoUrl { get; init; }
    public string Origin { get; init; }
}