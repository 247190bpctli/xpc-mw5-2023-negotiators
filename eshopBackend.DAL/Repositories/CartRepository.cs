using System.Data;
using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Repositories;

public class CartRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<CartRepository> _logger;

    public CartRepository(AppDbContext db, ILogger<CartRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public CartEntity CartDetails(Guid cartId)
    {
        return _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;
    }

    public Guid CartAdd()
    {
        //assemble the row
        CartEntity newCart = new()
        {
            Products = new List<ProductEntity>()
        };

        //add row to db
        DbSet<CartEntity> cartUpdate = _db.Set<CartEntity>();

        cartUpdate.Add(newCart);
        _db.SaveChanges();
            
        return newCart.Id;
    }
    
    public void CartEdit(Guid cartId, int deliveryType, string deliveryAddress, int paymentType, string paymentDetails)
    {
        
        CartEntity cartToEdit = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;
        
        cartToEdit.DeliveryType = deliveryType;
        cartToEdit.DeliveryAddress = deliveryAddress;
        cartToEdit.PaymentType = paymentType;
        cartToEdit.PaymentDetails = paymentDetails;
        
        _db.SaveChanges();
    }

    public void CartDelete(Guid cartId)
    {
        CartEntity cartToDelete = _db.Carts.SingleOrDefault(cart => cart.Id == cartId)!;

        _db.Carts.Remove(cartToDelete);
        _db.SaveChanges();
    }

    public void AddToCart(Guid cartId, Guid productId, int amount)
    {
        CartEntity cart = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;

        //we don't need category and manufacturer here
        ProductEntity product = _db.Products.SingleOrDefault(product => product.Id == productId)!;
        ProductInCartEntity productWithAmount = (ProductInCartEntity)product;
        productWithAmount.Amount = amount;

        cart.Products.Add(product);
        _db.SaveChanges();
    }

    public void FinalizeOrder(Guid cartId)
    {
        CartEntity cart = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;

        PlacedOrderEntity placedOrder = new()
        {
            Products = cart.Products,
            DeliveryType = cart.DeliveryType,
            DeliveryAddress = cart.DeliveryAddress,
            PaymentType = cart.PaymentType,
            PaymentDetails = cart.PaymentDetails,
            Timestamp = DateTime.Now 
        };

        _db.PlacedOrders.Add(placedOrder);
        _db.Carts.Remove(cart);
        _db.SaveChanges();
    }
}