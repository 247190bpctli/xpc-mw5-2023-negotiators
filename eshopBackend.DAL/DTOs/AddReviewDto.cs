namespace eshopBackend.DAL.DTOs;

public record AddReviewDto
{
    public double Stars { get; set; }
    public string User { get; init; }
    public string Description { get; init; }
}
