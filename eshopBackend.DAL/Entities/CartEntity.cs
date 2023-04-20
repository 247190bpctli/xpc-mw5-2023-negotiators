namespace eshopBackend.DAL.Entities;

public class CartEntity : BaseEntity
{
    public required List<ProductEntity> Products { get; set; }

    public int? DeliveryType { get; set; }
    
    public string? DeliveryAddress { get; set; }
    
    public int? PaymentType { get; set; }
    
    public string? PaymentDetails { get; set; }
}