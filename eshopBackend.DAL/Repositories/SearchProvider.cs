using System.Data;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Repositories;

public class SearchProvider
{
    private readonly DbConnectorFactory _db;
    private readonly ILogger<SearchProvider> _logger;

    public SearchProvider(DbConnectorFactory db, ILogger<SearchProvider> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public List<CategoryEntity>? SearchCategoryByName(string searchTerm)
    {
        try
        {
            List<CategoryEntity> foundCategories = _db.Categories.Where(category => category.Name.Contains(searchTerm)).ToList();
            
            return foundCategories;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Cannot look for category: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Cannot look for category: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    public List<ManufacturerEntity>? SearchManufacturerByName(string searchTerm)
    {
        try
        {
            List<ManufacturerEntity> foundManufacturers = _db.Manufacturers.Where(manufacturer => manufacturer.Name.Contains(searchTerm)).ToList();
            
            return foundManufacturers;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Cannot look for manufacturer: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Cannot look for manufacturer: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public List<EntityProduct>? SearchProductByName(string searchTerm)
    {
        try
        {
            List<EntityProduct> foundProducts = _db.Products
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Reviews)
                .Where(product => product.Name.Contains(searchTerm)).ToList();
            
            return foundProducts;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Cannot look for product: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Cannot look for product: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
}