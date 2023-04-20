namespace eshopBackend.DAL.Entities;

public class PlacedOrderEntity : BaseEntity
{
    public required List<ProductEntity> Products { get; set; }

    public int? DeliveryType { get; set; }
    
    public string? DeliveryAddress { get; set; }
    
    public int? PaymentType { get; set; }
    
    public string? PaymentDetails { get; set; }
    
    public required DateTime Timestamp { get; set; }
}