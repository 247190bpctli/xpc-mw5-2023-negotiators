namespace eshopBackend.DAL.Entities;

public abstract record BaseEntity : IEntity
{
    public required Guid Id { get; init; }
}