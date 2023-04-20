using System.Data;
using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Repositories;

public class ManufacturerRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<ManufacturerRepository> _logger;

    public ManufacturerRepository(AppDbContext db, ILogger<ManufacturerRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public List<ManufacturerEntity>? ManufacturersOverview(byte page = 1)
    {
        try
        {
            int skipRange = (page - 1) * 25;
            List<ManufacturerEntity> manufacturers = _db.Manufacturers.Skip(skipRange).Take(25).ToList();

            return manufacturers;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Manufacturers cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Manufacturers cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public ManufacturerEntity? ManufacturerDetails(Guid id)
    {
        try
        {
            ManufacturerEntity manufacturer = _db.Manufacturers.Single(manufacturer => manufacturer.Id == id);

            return manufacturer;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Manufacturer cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Manufacturer cannot be displayed: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }

    public Guid? ManufacturerAdd(string name, string? description = null, string? logoUrl = null, string? origin = null)
    {
        try
        {
            //generate guid
            Guid newManufacturerGuid = Guid.NewGuid();
            
            //assemble the row
            ManufacturerEntity newManufacturer = new()
            {
                Id = newManufacturerGuid,
                Name = name,
                Description = description,
                LogoUrl = logoUrl,
                Origin = origin
            };
            
            //add row to db
            DbSet<ManufacturerEntity> manufacturerUpdate = _db.Set<ManufacturerEntity>();

            manufacturerUpdate.Add(newManufacturer);
            _db.SaveChanges();
            return newManufacturerGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Manufacturer cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Manufacturer cannot be added: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    public bool ManufacturerEdit(Guid id, string? name = null, string? description = null, string? logoUrl = null, string? origin = null)
    {
        try
        {
            ManufacturerEntity manufacturerToEdit = _db.Manufacturers.Single(manufacturer => manufacturer.Id == id);

            if (name != null)
            {
                manufacturerToEdit.Name = name;
            }
            
            if (description != null)
            {
                manufacturerToEdit.Description = description;
            }
        
            if (logoUrl != null)
            {
                manufacturerToEdit.LogoUrl = logoUrl;
            }
        
            if (origin != null)
            {
                manufacturerToEdit.Origin = origin;
            }

            _db.SaveChanges();
            
            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.LogError("Manufacturer cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError("Manufacturer cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Manufacturer cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Manufacturer cannot be edited: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    public bool ManufacturerDelete(Guid id)
    {
        try
        {
            IQueryable<ManufacturerEntity> manufacturerToDelete = _db.Manufacturers.Where(manufacturer => manufacturer.Id == id);

            _db.Manufacturers.RemoveRange(manufacturerToDelete);
            _db.SaveChanges();
            
            return true;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Manufacturer cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Manufacturer cannot be deleted: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }
}