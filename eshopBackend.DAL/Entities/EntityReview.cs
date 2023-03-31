namespace eshopBackend.DAL.Entities;

public class EntityReview : EntityBase
{
    public required string Stars { get; set; }
    
    public required string Name { get; set; }

    public string? Description { get; set; }
}