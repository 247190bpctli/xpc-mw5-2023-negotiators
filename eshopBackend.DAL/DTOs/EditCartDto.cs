namespace eshopBackend.DAL.DTOs;

public record EditCartDto
{
    public Guid CartId { get; init; }
    public int? DeliveryType { get; init; }
    public string DeliveryAddress { get; init; }
    public int? PaymentType { get; init; }
    public string PaymentDetails { get; init; }
}
