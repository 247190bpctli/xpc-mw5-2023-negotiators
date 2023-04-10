using System.Data;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Services;

public class SearchProvider
{
    private readonly DbConnectorFactory _db;
    private readonly LoggerFactory _logger;

    public SearchProvider(DbConnectorFactory db, LoggerFactory logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public List<EntityCategory>? SearchCategoryByName(string searchTerm)
    {
        try
        {
            List<EntityCategory> foundCategories = _db.Categories.Where(category => category.Name.Contains(searchTerm)).ToList();
            
            return foundCategories;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cannot look for category: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cannot look for category: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    public List<EntityManufacturer>? SearchManufacturerByName(string searchTerm)
    {
        try
        {
            List<EntityManufacturer> foundManufacturers = _db.Manufacturers.Where(manufacturer => manufacturer.Name.Contains(searchTerm)).ToList();
            
            return foundManufacturers;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cannot look for manufacturer: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cannot look for manufacturer: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public List<EntityProduct>? SearchProductByName(string searchTerm)
    {
        try
        {
            List<EntityProduct> foundProducts = _db.Products.Where(product => product.Name.Contains(searchTerm)).ToList();
            
            return foundProducts;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Cannot look for product: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Cannot look for product: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
}