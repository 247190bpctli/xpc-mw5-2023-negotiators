using System.Data;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LoggerFactory = eshopBackend.DAL.Factories.LoggerFactory;

namespace eshopBackend.DAL.Services;

public class Cart
{
    private readonly DbConnectorFactory _db;
    private readonly LoggerFactory _logger;

    public Cart(DbConnectorFactory db, LoggerFactory logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public EntityCart? CartDetails(Guid cartId)
    {
        try
        {
            EntityCart cart = _db.Carts.Single(cart => cart.Id == cartId);

            return cart;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cart cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cart cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public Guid? CartAdd()
    {
        try
        {
            //generate guid
            Guid newCartGuid = Guid.NewGuid();
            
            //assemble the row
            EntityCart newCart = new()
            {
                Id = newCartGuid,
                Products = new List<EntityProduct>()
            };
            
            //add row to db
            DbSet<EntityCart> cartUpdate = _db.Set<EntityCart>();

            cartUpdate.Add(newCart);
            _db.SaveChanges();
            return newCartGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cart cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cart cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    public bool CartEdit(Guid cartId, int? deliveryType, string? deliveryAddress, int? paymentType, string? paymentDetails)
    {
        try
        {
            EntityCart cartToEdit = _db.Carts.Single(cart => cart.Id == cartId);

            if (deliveryType != null)
            {
                cartToEdit.DeliveryType = deliveryType;
            }
        
            if (deliveryAddress != null)
            {
                cartToEdit.DeliveryAddress = deliveryAddress;
            }
            
            if (paymentType != null)
            {
                cartToEdit.PaymentType = paymentType;
            }
        
            if (paymentDetails != null)
            {
                cartToEdit.PaymentDetails = paymentDetails;
            }

            _db.SaveChanges();
            
            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.Log.LogError("Cart cannot be edited: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (InvalidOperationException e)
        {
            _logger.Log.LogError("Cart cannot be edited: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cart cannot be edited: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cart cannot be edited: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    public bool CartDelete(Guid cartId)
    {
        try
        {
            IQueryable<EntityCart> cartToDelete = _db.Carts.Where(cart => cart.Id == cartId);

            _db.Carts.RemoveRange(cartToDelete);
            _db.SaveChanges();
            
            return true;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cart cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cart cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    public bool AddToCart(Guid cartId, Guid productId, int amount)
    {
        try
        {
            EntityCart cart = _db.Carts.Single(cart => cart.Id == cartId);

            cart.Products.Add(_db.Products.Single(product => product.Id == productId));
            _db.SaveChanges();
            
            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.Log.LogError("Cannot add product to cart: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (InvalidOperationException e)
        {
            _logger.Log.LogError("Cannot add product to cart: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cannot add product to cart: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cannot add product to cart: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    public bool FinalizeOrder(Guid cartId)
    {
        try
        {
            EntityCart cart = _db.Carts.Single(cart => cart.Id == cartId);

            if (cart.DeliveryType == null || cart.PaymentType == null)
            {
                _logger.Log.LogError("Order cannot be finalized: Required parameters are not set!");
                return false;
            }

            EntityPlacedOrder placedOrder = new()
            {
                Id = cart.Id,
                Products = cart.Products,
                DeliveryType = cart.DeliveryType,
                DeliveryAddress = cart.DeliveryAddress,
                PaymentType = cart.PaymentType,
                PaymentDetails = cart.PaymentDetails,
                Timestamp = DateTime.Now
            };

            _db.PlacedOrders.Add(placedOrder);
            _db.Carts.RemoveRange(cart);
            _db.SaveChanges();
            
            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.Log.LogError("Order cannot be finalized: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (InvalidOperationException e)
        {
            _logger.Log.LogError("Order cannot be finalized: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Order cannot be finalized: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Order cannot be finalized: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }
}