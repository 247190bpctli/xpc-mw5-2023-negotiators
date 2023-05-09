namespace eshopBackend.DAL.DTOs;

public record AddReviewDto
{
    public Guid ProductId { get; init; }
    public double Stars { get; set; }
    public string User { get; init; }
    public string? Description { get; init; }
}
