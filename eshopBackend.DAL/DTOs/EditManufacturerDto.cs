namespace eshopBackend.DAL.DTOs;

public record EditManufacturerDto
{
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string LogoUrl { get; init; }
    public string Origin { get ; init; }
}
