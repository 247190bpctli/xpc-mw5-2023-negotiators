namespace eshopBackend.DAL.Entities;

public abstract class EntityBase : IEntity
{
    public Guid Id { get; set; }
}