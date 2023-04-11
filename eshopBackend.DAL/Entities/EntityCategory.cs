namespace eshopBackend.DAL.Entities;

public record EntityCategory : EntityBase
{
    public required string Name { get; set; }
}