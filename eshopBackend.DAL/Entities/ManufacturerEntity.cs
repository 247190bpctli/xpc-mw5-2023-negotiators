namespace eshopBackend.DAL.Entities;

public class ManufacturerEntity : BaseEntity
{
    public required string Name { get; set; }

    public string Description { get; set; } = null!;

    public string LogoUrl { get; set; } = null!;

    public string Origin { get; set; } = null!;
}