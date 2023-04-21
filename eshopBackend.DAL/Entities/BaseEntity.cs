namespace eshopBackend.DAL.Entities;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; init; }
}