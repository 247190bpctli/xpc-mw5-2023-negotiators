using System.Data;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Services;

public class Products
{
    private readonly DbConnectorFactory _db;
    private readonly ILogger<Products> _logger;

    public Products(DbConnectorFactory db, ILogger<Products> logger)
    {
        _db = db;
        _logger = logger;
    }

    public List<EntityProduct>? ProductsOverview(byte page = 1)
    {
        try
        {
            int skipRange = (page - 1) * 25;
            List<EntityProduct> products = _db.Products.Skip(skipRange).Take(25)
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Reviews)
                .ToList();

            return products;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Products cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Products cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public EntityProduct? ProductDetails(Guid id)
    {
        try
        {
            EntityProduct product = _db.Products
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Reviews)
                .Single(product => product.Id == id);

            return product;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Product cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Product cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public Guid? ProductAdd(string name, string? imageUrl, string? description, double price, double weight, int stock, Guid? categoryId, Guid? manufacturerId)
    {
        try
        {
            //generate guid
            Guid newProductGuid = Guid.NewGuid();
            
            //assemble the row
            EntityProduct newProduct = new()
            {
                Id = newProductGuid,
                Name = name,
                ImageUrl = imageUrl,
                Description = description,
                Price = price,
                Weight = weight,
                Stock = stock,
                Category = _db.Categories.SingleOrDefault(category => category.Id == categoryId), //todo null handle?
                Manufacturer = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == manufacturerId), //todo null handle?
                Reviews = new List<EntityReview>()
            };
            
            //add row to db
            DbSet<EntityProduct> productUpdate = _db.Set<EntityProduct>();

            productUpdate.Add(newProduct);
            _db.SaveChanges();
            return newProductGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Product cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Product cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    public bool ProductEdit(Guid id, string? name = null, string? imageUrl = null, string? description = null, double? price = null, double? weight = null, int? stock = null, Guid? categoryId = null, Guid? manufacturerId = null)
    {
        try
        {
            EntityProduct productToEdit = _db.Products.Single(product => product.Id == id);

            if (name != null)
            {
                productToEdit.Name = name;
            }
            
            if (imageUrl != null)
            {
                productToEdit.ImageUrl = imageUrl;
            }
            
            if (description != null)
            {
                productToEdit.Description = description;
            }
        
            if (price != null)
            {
                productToEdit.Price = (double)price;
            }
        
            if (weight != null)
            {
                productToEdit.Weight = (double)weight;
            }
            
            if (stock != null)
            {
                productToEdit.Stock = (int)stock;
            }
            
            if (categoryId != null)
            {
                productToEdit.Category = _db.Categories.SingleOrDefault(category => category.Id == categoryId); //todo null handle?
            }
            
            if (manufacturerId != null)
            {
                productToEdit.Manufacturer = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == manufacturerId); //todo null handle?
            }

            _db.SaveChanges();
            
            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.LogError("Product cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError("Product cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Product cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Product cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    public bool ProductDelete(Guid id)
    {
        try
        {
            IQueryable<EntityProduct> productToDelete = _db.Products.Where(product => product.Id == id);

            _db.Products.RemoveRange(productToDelete);
            _db.SaveChanges();
            
            return true;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Product cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Product cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    public bool ReviewAdd(Guid productId, byte stars, string user, string? description = null)
    {
        try
        {
            //assemble the row
            EntityReview newReview = new()
            {
                Id = Guid.NewGuid(),
                Stars = stars,
                User = user,
                Description = description
            };
            
            //add row to db
            DbSet<EntityProduct> productUpdate = _db.Set<EntityProduct>();

            productUpdate
                .Include(x => x.Reviews)
                .Single(product => product.Id == productId).Reviews.Add(newReview);
            _db.SaveChanges();
            
            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.LogError("Review cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError("Review cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Review cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Review cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }
}