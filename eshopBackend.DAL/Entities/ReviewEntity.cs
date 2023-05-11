namespace eshopBackend.DAL.Entities;

public class ReviewEntity : BaseEntity
{
    public required double Stars { get; set; }

    public required string User { get; set; }

    public required string Description { get; set; }
}