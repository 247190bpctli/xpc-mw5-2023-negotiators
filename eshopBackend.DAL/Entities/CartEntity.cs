namespace eshopBackend.DAL.Entities;

public class CartEntity : BaseEntity
{
    public List<ProductEntity> Products { get; set; } = null!;

    public int DeliveryType { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public int PaymentType { get; set; }

    public string PaymentDetails { get; set; } = null!;

    public required DateTime LastEdit { get; set; }

    public required bool Finalized { get; set; }
}