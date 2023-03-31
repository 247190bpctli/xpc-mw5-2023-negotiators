namespace eshopBackend.DAL.Entities;

public class EntityManufacturer : EntityBase
{
    public required string Name { get; set; }

    public string? Description { get; set; }
    
    public string? LogoUrl { get; set; }
    
    public string? Origin { get; set; }
}