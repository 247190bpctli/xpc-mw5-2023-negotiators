namespace eshopBackend.DAL.Entities;

public record EntityCategory : EntityBase
{
    public required string Name { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? Description { get; set; }
}