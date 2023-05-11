namespace eshopBackend.DAL.Entities;

public class CategoryEntity : BaseEntity
{
    public required string Name { get; set; }
    
    public required string ImageUrl { get; set; }

    public required string Description { get; set; }
}