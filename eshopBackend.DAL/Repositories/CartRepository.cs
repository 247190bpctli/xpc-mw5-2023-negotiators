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
    
    public void CartEdit(EditCartDto editCartDto)
    {
        
        CartEntity cartToEdit = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == editCartDto.CartId)!;
        
        cartToEdit.DeliveryType = editCartDto.DeliveryType;
        cartToEdit.DeliveryAddress = editCartDto.DeliveryAddress;
        cartToEdit.PaymentType = editCartDto.PaymentType;
        cartToEdit.PaymentDetails = editCartDto.PaymentDetails;
        cartToEdit.LastEdit = DateTime.Now;
        
        _db.SaveChanges();
    }

    public void CartDelete(Guid cartId)
    {
        CartEntity cartToDelete = _db.Carts.SingleOrDefault(cart => cart.Id == cartId)!;

        _db.Carts.Remove(cartToDelete);
        _db.SaveChanges();
    }

    public void AddToCart(AddToCartDto addToCartDto)
    {
        CartEntity cart = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == addToCartDto.CartId)!;

        cart.LastEdit = DateTime.Now;

        //we don't need category and manufacturer here
        ProductEntity product = _db.Products.SingleOrDefault(product => product.Id == addToCartDto.ProductId)!;
        ProductInCartEntity productWithAmount = (ProductInCartEntity)product;
        productWithAmount.Amount = addToCartDto.Amount;

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