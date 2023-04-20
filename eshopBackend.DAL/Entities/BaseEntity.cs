namespace eshopBackend.DAL.Entities;

public abstract class BaseEntity : IEntity
{
    public required Guid Id { get; init; }
}