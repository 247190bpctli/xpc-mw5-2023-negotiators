using Google.Protobuf.WellKnownTypes;

namespace eshopBackend.DAL.Entities;

public record EntityPlacedOrder : EntityBase
{
    public required List<EntityProduct> Products { get; set; }

    public int? DeliveryType { get; set; }
    
    public string? DeliveryAddress { get; set; }
    
    public int? PaymentType { get; set; }
    
    public string? PaymentDetails { get; set; }
    
    public required DateTime Timestamp { get; set; }
}