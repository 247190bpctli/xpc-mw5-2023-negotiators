using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

[Table("manufacturers")]
public class EntityManufacturer : EntityBase
{
    [Column, NotNull]
    public required string Name { get; set; }

    [Column]
    public string? Description { get; set; }
    
    [Column]
    public string? LogoUrl { get; set; }
    
    [Column]
    public string? Origin { get; set; }
}