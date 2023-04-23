using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL.Repositories;

public class CartRepository
{
    private readonly AppDbContext _db;

    public CartRepository(AppDbContext db) => _db = db;

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
            Products = new List<ProductEntity>(),
            LastEdit = DateTime.Now,
            Finalized = false
        };

        //add row to db
        DbSet<CartEntity> cartUpdate = _db.Set<CartEntity>();

        cartUpdate.Add(newCart);
        _db.SaveChanges();
            
        return newCart.Id;
    }
    
    public void CartEdit(CartEditDTO c)
    {
        
        CartEntity cartToEdit = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == c.CartId)!;
        
        cartToEdit.DeliveryType = c.DeliveryType;
        cartToEdit.DeliveryAddress = c.DeliveryAddress;
        cartToEdit.PaymentType = c.PaymentType;
        cartToEdit.PaymentDetails = c.PaymentDetails;
        cartToEdit.LastEdit = DateTime.Now;
        
        _db.SaveChanges();
    }

    public void CartDelete(Guid cartId)
    {
        CartEntity cartToDelete = _db.Carts.SingleOrDefault(cart => cart.Id == cartId)!;

        _db.Carts.Remove(cartToDelete);
        _db.SaveChanges();
    }

    public void AddToCart(AddToCartDTO a)
    {
        CartEntity cart = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == a.CartId)!;

        cart.LastEdit = DateTime.Now;

        //we don't need category and manufacturer here
        ProductEntity product = _db.Products.SingleOrDefault(product => product.Id == a.ProductId)!;
        ProductInCartEntity productWithAmount = (ProductInCartEntity)product;
        productWithAmount.Amount = a.Amount;

        cart.Products.Add(product);
        _db.SaveChanges();
    }

    public void FinalizeOrder(Guid cartId)
    {
        CartEntity cart = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;

        if (cart is
            {
                DeliveryType: not null,
                DeliveryAddress: not null,
                PaymentType: not null,
                PaymentDetails: not null
            })
        {
            cart.Finalized = true;
        }

        _db.SaveChanges();
    }
}