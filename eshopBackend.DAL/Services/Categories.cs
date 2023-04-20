using System.Data;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Services;

public class Categories
{
    private readonly DbConnectorFactory _db;
    private readonly ILogger<Categories> _logger;

    public Categories(DbConnectorFactory db, ILogger<Categories> logger)
    {
        _db = db;
        _logger = logger;
    }

    public List<CategoryEntity>? CategoriesOverview(byte page = 1)
    {
        try
        {
            int skipRange = (page - 1) * 25;
            List<CategoryEntity> categories = _db.Categories.Skip(skipRange).Take(25).ToList();

            return categories;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Categories cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Categories cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public CategoryEntity? CategoryDetails(Guid id)
    {
        try
        {
            CategoryEntity category = _db.Categories.Single(category => category.Id == id);

            return category;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Category cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Category cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public Guid? CategoryAdd(string name, string? imageUrl = null, string? description = null)
    {
        try
        {
            //generate guid
            Guid newCategoryGuid = Guid.NewGuid();
            
            //assemble the row
            CategoryEntity newCategory = new()
            {
                Id = newCategoryGuid,
                Name = name,
                ImageUrl = imageUrl,
                Description = description
            };
            
            //add row to db
            DbSet<CategoryEntity> categoryUpdate = _db.Set<CategoryEntity>();

            categoryUpdate.Add(newCategory);
            _db.SaveChanges();
            return newCategoryGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Category cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Category cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public bool CategoryEdit(Guid id, string? name = null, string? imageUrl = null, string? description = null)
    {
        try
        {
            CategoryEntity categoryToEdit = _db.Categories.Single(category => category.Id == id);

            if (name != null)
            {
                categoryToEdit.Name = name;
            }
        
            if (imageUrl != null)
            {
                categoryToEdit.ImageUrl = imageUrl;
            }
        
            if (description != null)
            {
                categoryToEdit.Description = description;
            }

            _db.SaveChanges();
            
            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.LogError("Category cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError("Category cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Category cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Category cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    public bool CategoryDelete(Guid id)
    {
        try
        {
            IQueryable<CategoryEntity> categoryToDelete = _db.Categories.Where(category => category.Id == id);

            _db.Categories.RemoveRange(categoryToDelete);
            _db.SaveChanges();
            
            return true;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Category cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Category cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }
}