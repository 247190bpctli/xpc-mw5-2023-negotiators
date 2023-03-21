using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

[Table("ratings")]
public class EntityRating : EntityBase
{
    [Column, NotNull]
    public required string Stars { get; set; }
    
    [Column]
    public required string Text { get; set; }
}