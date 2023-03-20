using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

public abstract class EntityBase : IEntity
{
    [PrimaryKey, Identity]
    public required Guid Id { get; init; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
    }
}