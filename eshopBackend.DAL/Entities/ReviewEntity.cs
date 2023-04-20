namespace eshopBackend.DAL.Entities;

public record EntityReview : BaseEntity
{
    public required byte Stars { get; set; }
    
    public required string User { get; set; }

    public string? Description { get; set; }
}