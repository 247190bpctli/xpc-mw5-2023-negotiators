using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

[Table("products")]
public class EntityProduct : EntityBase
{
    [Column, NotNull]
    public required string Name { get; set; }
    
    [Column]
    public string? ImageUrl { get; set; }
    
    [Column]
    public string? Description { get; set; }
    
    [Column]
    public float Price { get; set; }
    
    [Column]
    public float Weight { get; set; }
    
    [Column]
    public int Stock { get; set; }
    
    [Column]
    public int CategoryId { get; set; }
    
    [Association(ThisKey = nameof(CategoryId), OtherKey=nameof(EntityCategory.Id))]
    public EntityCategory? Category { get; set; }
    
    [Column]
    public int ManufacturerId { get; set; }
    
    [Association(ThisKey = nameof(ManufacturerId), OtherKey=nameof(EntityManufacturer.Id))]
    public EntityManufacturer? Manufacturer { get; set; }
    
    [Column]
    public int ReviewId { get; set; }
    
    [Association(ThisKey = nameof(ReviewId), OtherKey=nameof(EntityReview.Id))]
    public EntityReview? Review { get; set; }
}