using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

[Table("vendors")]
public class EntityVendor : EntityBase
{
    [Column, NotNull]
    public required string Name { get; set; }
}