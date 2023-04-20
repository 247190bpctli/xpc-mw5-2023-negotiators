namespace eshopBackend.DAL.Entities;

public record CategoryEntity : BaseEntity
{
    public required string Name { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? Description { get; set; }
}