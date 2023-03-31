namespace eshopBackend.DAL.Entities;

public abstract class EntityBase : IEntity
{
    public required Guid Id { get; init; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
    }
}