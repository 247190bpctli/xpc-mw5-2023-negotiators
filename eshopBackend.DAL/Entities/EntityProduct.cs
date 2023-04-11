namespace eshopBackend.DAL.Entities;

public record EntityProduct : EntityBase
{
    public required string Name { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? Description { get; set; }
    
    public float Price { get; set; }
    
    public float Weight { get; set; }
    
    public int Stock { get; set; }
    
    public EntityCategory? Category { get; set; }
    
    public EntityManufacturer? Manufacturer { get; set; }
    
    public EntityReview[]? Reviews { get; set; }
}