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
    public float Mass { get; set; }
    
    [Column]
    public int Stock { get; set; }
    
    [Column]
    public int CategoryId { get; set; }
    
    [Association(ThisKey = nameof(CategoryId), OtherKey=nameof(EntityCategory.Id))]
    public EntityCategory? Category { get; set; }
    
    [Column]
    public int VendorId { get; set; }
    
    [Association(ThisKey = nameof(VendorId), OtherKey=nameof(EntityVendor.Id))]
    public EntityVendor? Vendor { get; set; }
    
    [Column]
    public int RatingId { get; set; }
    
    [Association(ThisKey = nameof(RatingId), OtherKey=nameof(EntityRating.Id))]
    public EntityRating? Rating { get; set; }
}