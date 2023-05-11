namespace eshopBackend.DAL.DTOs;

public record AddReviewDto
{
    public double Stars { get; set; }
    public string User { get; init; } = null!;
    public string Description { get; init; } = null!;
}