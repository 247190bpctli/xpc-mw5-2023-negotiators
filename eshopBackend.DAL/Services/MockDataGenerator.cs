using System.Data;
using Bogus;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Services;

public class MockDataGenerator
{
    private readonly DbConnectorFactory _db;
    private readonly LoggerFactory _logger;

    public MockDataGenerator(DbConnectorFactory db, LoggerFactory logger)
    {
        _db = db;
        _logger = logger;
    }

    private Guid? MakeMockCategory(int? seed = null) //todo call createsvc??
    {
        try
        {
            Guid mockGuid = Guid.NewGuid();
            
            if (seed != null)
            {
                Randomizer.Seed = new Random((int)seed);
            }

            EntityCategory bogusCategory = new Faker<EntityCategory>()
                .RuleFor(o => o.Id, f => mockGuid)
                .RuleFor(o => o.Name, f => f.Vehicle.Type())
                .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
                .RuleFor(o => o.Description, f => f.Lorem.Lines(1));

            _db.Categories.Add(bogusCategory);
            _db.SaveChanges();

            return mockGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Mock category cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Mock category cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    private Guid? MakeMockManufacturer(int? seed = null) //todo call createsvc??
    {
        try
        {
            Guid mockGuid = Guid.NewGuid();
            
            if (seed != null)
            {
                Randomizer.Seed = new Random((int)seed);
            }
            
            EntityManufacturer bogusManufacturer = new Faker<EntityManufacturer>()
                .RuleFor(o => o.Id, f => mockGuid)
                .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
                .RuleFor(o => o.Description, f => f.Lorem.Lines(1))
                .RuleFor(o => o.LogoUrl, f => f.Image.DataUri(200, 100))
                .RuleFor(o => o.Origin, f => f.Address.Country());

            _db.Manufacturers.Add(bogusManufacturer);
            _db.SaveChanges();

            return mockGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Mock manufacturer cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Mock manufacturer cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    private Guid? MakeMockProduct(Guid categoryId, Guid manufacturerId, int? seed = null) //todo call createsvc??
    {
        try
        {
            Guid mockGuid = Guid.NewGuid();

            EntityCategory category = _db.Categories.Single(category => category.Id == categoryId);
            EntityManufacturer manufacturer = _db.Manufacturers.Single(manufacturer => manufacturer.Id == manufacturerId);
            
            if (seed != null)
            {
                Randomizer.Seed = new Random((int)seed);
            }

            EntityProduct bogusProduct = new Faker<EntityProduct>()
                .RuleFor(o => o.Id, f => mockGuid)
                .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
                .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
                .RuleFor(o => o.Description, f => f.Lorem.Lines(1));

            bogusProduct.Category = category;
            bogusProduct.Manufacturer = manufacturer;
            bogusProduct.Reviews = new List<EntityReview>();
            
            _db.Products.Add(bogusProduct);
            _db.SaveChanges();

            return mockGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.Log.LogError("Mock product cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.Log.LogError("Mock product cannot be created: {ExceptionMsg}", e.Message);
            _logger.Log.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    public bool MakeMockData(byte dataAmount, int? seed = null)
    {
        for (int i = 0; i < dataAmount; i++)
        {
            Guid? categoryId = MakeMockCategory(seed);
            Guid? manufacturerId = MakeMockManufacturer(seed);

            if (categoryId != null && manufacturerId != null)
            {
                Guid? product = MakeMockProduct((Guid)categoryId, (Guid)manufacturerId, seed);
                _logger.Log.LogDebug("Mock data created");
            }
            else
            {
                _logger.Log.LogError("Mock data creation error - check logs above!");
                return false;
            }
        }
        
        return true;
    }
}