using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

[Table("categories")]
public class EntityCategory : EntityBase
{
    [Column, NotNull]
    public required string Name { get; set; }
}