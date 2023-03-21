using LinqToDB.Mapping;

namespace eshopBackend.DAL.Entities;

public abstract class EntityBase : IEntity
{
    [PrimaryKey, Column]
    public required Guid Id { get; init; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
    }
}