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
            DeliveryType = default,
            DeliveryAddress = string.Empty,
            PaymentType = default,
            PaymentDetails = string.Empty,
            Finalized = false
        };

        //add row to db
        DbSet<CartEntity> cartUpdate = _db.Set<CartEntity>();

        cartUpdate.Add(newCart);
        _db.SaveChanges();

        return newCart.Id;
    }

    public void CartEdit(Guid cartId, EditCartDto editCartDto)
    {
        CartEntity cartToEdit = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;

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

    public void AddToCart(Guid cartId, AddToCartDto addToCartDto)
    {
        CartEntity cart = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;

        cart.LastEdit = DateTime.Now;

        //we don't need category and manufacturer here
        ProductEntity product = _db.Products.SingleOrDefault(product => product.Id == addToCartDto.ProductId)!;

        ProductInCartEntity productWithAmount = new()
        {
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Description = product.Description,
            Price = product.Price,
            Weight = product.Weight,
            Stock = product.Stock,
            Category = product.Category,
            CategoryId = product.CategoryId,
            Manufacturer = product.Manufacturer,
            ManufacturerId = product.ManufacturerId,
            Reviews = product.Reviews,
            Amount = addToCartDto.Amount
        };

        cart.Products.Add(productWithAmount);
        _db.SaveChanges();
    }

    public void FinalizeOrder(Guid cartId)
    {
        CartEntity cart = _db.Carts
            .Include(x => x.Products)
            .SingleOrDefault(cart => cart.Id == cartId)!;

        if (cart is
            {
                DeliveryType: not default(int),
                DeliveryAddress: not "",
                PaymentType: not default(int),
                PaymentDetails: not ""
            })
        {
            cart.Finalized = true;
        }
        else
        {
            throw new InvalidOperationException("Cart cannot be finalized without required parameters");
        }

        _db.SaveChanges();
    }
}