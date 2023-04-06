namespace eshopBackend.DAL.Entities;

public abstract record EntityBase : IEntity
{
    public required Guid Id { get; init; }
}