namespace eshopBackend.DAL.Entities;

public record EntityProduct : EntityBase
{
    public required string Name { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? Description { get; set; }
    
    public double Price { get; set; }
    
    public double Weight { get; set; }
    
    public int Stock { get; set; }
    
    public EntityCategory? Category { get; set; }
    
    public EntityManufacturer? Manufacturer { get; set; }
    
    public required List<EntityReview> Reviews { get; set; }
}