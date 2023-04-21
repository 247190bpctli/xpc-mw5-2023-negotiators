using System.Data;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(AppDbContext db, ILogger<ProductRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public List<ProductEntity>? ProductsOverview(uint page = 1)
    {
        try
        {
            page = page is <= 255 and > 0 ? page : 255; //limit pages to 255 without zero
            uint skipRange = (page - 1) * 25;
            List<ProductEntity> products = _db.Products.Skip((int)skipRange).Take(25)
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

    public ProductEntity? ProductDetails(Guid id)
    {
        try
        {
            ProductEntity product = _db.Products
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

    public Guid? ProductAdd(ProductAddDto p)
    {
        try
        {
            //generate guid
            Guid newProductGuid = Guid.NewGuid();
            
            //assemble the row
            ProductEntity newProduct = new()
            {
                Id = newProductGuid,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Price = p.Price,
                Weight = p.Weight,
                Stock = p.Stock,
                Category = _db.Categories.SingleOrDefault(category => category.Id == p.CategoryId), //todo null handle?
                Manufacturer = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == p.ManufacturerId), //todo null handle?
                Reviews = new List<ReviewEntity>()
            };
            
            //add row to db
            DbSet<ProductEntity> productUpdate = _db.Set<ProductEntity>();

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
            ProductEntity productToEdit = _db.Products.Single(product => product.Id == id);

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
            IQueryable<ProductEntity> productToDelete = _db.Products.Where(product => product.Id == id);

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

    public bool ReviewAdd(Guid productId, double stars, string user, string? description = null)
    {
        stars = (stars <= 5) ? stars : 5; //limit stars to 5
        try
        {
            //assemble the row
            ReviewEntity @new = new()
            {
                Id = Guid.NewGuid(),
                Stars = stars,
                User = user,
                Description = description
            };
            
            //add row to db
            DbSet<ProductEntity> productUpdate = _db.Set<ProductEntity>();

            productUpdate
                .Include(x => x.Reviews)
                .Single(product => product.Id == productId).Reviews.Add(@new);
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