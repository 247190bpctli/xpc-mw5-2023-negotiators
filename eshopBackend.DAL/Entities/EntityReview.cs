using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

[Table("reviews")]
public class EntityReview : EntityBase
{
    [Column, NotNull]
    public required string Stars { get; set; }
    
    [Column, NotNull]
    public required string Name { get; set; }

    [Column]
    public string? Description { get; set; }
}