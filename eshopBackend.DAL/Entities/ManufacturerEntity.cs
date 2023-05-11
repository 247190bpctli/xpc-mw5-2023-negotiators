namespace eshopBackend.DAL.Entities;

public class ManufacturerEntity : BaseEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string LogoUrl { get; set; }

    public required string Origin { get; set; }
}