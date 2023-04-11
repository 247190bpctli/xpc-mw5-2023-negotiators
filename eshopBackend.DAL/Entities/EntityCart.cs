namespace eshopBackend.DAL.Entities;

public record EntityCart : EntityBase
{
    public required List<EntityProduct> Products { get; set; }

    public int? DeliveryType { get; set; }
    
    public string? DeliveryAddress { get; set; }
    
    public int? PaymentType { get; set; }
    
    public string? PaymentDetails { get; set; }
}