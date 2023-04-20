using System.Data;
using Bogus;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Repositories;

public class MockDataGenerator
{
    private readonly DbConnectorFactory _db;
    private readonly ILogger<MockDataGenerator> _logger;

    public MockDataGenerator(DbConnectorFactory db, ILogger<MockDataGenerator> logger)
    {
        _db = db;
        _logger = logger;
    }

    private Guid? MakeMockCategory(int? seed = null)
    {
        try
        {
            Guid mockGuid = Guid.NewGuid();
            
            if (seed != null)
            {
                Randomizer.Seed = new Random((int)seed);
            }

            CategoryEntity bogusCategory = new Faker<CategoryEntity>()
                .RuleFor(o => o.Id, _ => mockGuid)
                .RuleFor(o => o.Name, f => f.Vehicle.Type())
                .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
                .RuleFor(o => o.Description, f => f.Lorem.Lines(1));

            _db.Categories.Add(bogusCategory);
            _db.SaveChanges();

            return mockGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Mock category cannot be created: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Mock category cannot be created: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    private Guid? MakeMockManufacturer(int? seed = null)
    {
        try
        {
            Guid mockGuid = Guid.NewGuid();
            
            if (seed != null)
            {
                Randomizer.Seed = new Random((int)seed);
            }
            
            ManufacturerEntity bogusManufacturer = new Faker<ManufacturerEntity>()
                .RuleFor(o => o.Id, _ => mockGuid)
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
            _logger.LogError("Mock manufacturer cannot be created: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Mock manufacturer cannot be created: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    private Guid? MakeMockProduct(Guid categoryId, Guid manufacturerId, int? seed = null)
    {
        try
        {
            Guid mockGuid = Guid.NewGuid();

            CategoryEntity category = _db.Categories.Single(category => category.Id == categoryId);
            ManufacturerEntity manufacturer = _db.Manufacturers.Single(manufacturer => manufacturer.Id == manufacturerId);
            
            if (seed != null)
            {
                Randomizer.Seed = new Random((int)seed);
            }

            EntityProduct bogusProduct = new Faker<EntityProduct>()
                .RuleFor(o => o.Id, _ => mockGuid)
                .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
                .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
                .RuleFor(o => o.Description, f => f.Lorem.Lines(1))
                .RuleFor(o => o.Price, f => f.Random.Number(499))
                .RuleFor(o => o.Weight, f => f.Random.Number(60))
                .RuleFor(o => o.Stock, f => f.Random.Number(5));

            bogusProduct.Category = category;
            bogusProduct.Manufacturer = manufacturer;
            bogusProduct.Reviews = new List<EntityReview>();
            
            _db.Products.Add(bogusProduct);
            _db.SaveChanges();

            return mockGuid;
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Mock product cannot be created: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
        catch (DBConcurrencyException e)
        {
            _logger.LogError("Mock product cannot be created: {ExceptionMsg}", e.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", e.StackTrace);
            return null;
        }
    }
    
    public bool MakeMockData(byte dataAmount, int? seed = null)
    {
        for (int i = 0; i < dataAmount; i++)
        {
            Guid? categoryId = MakeMockCategory(seed);
            Guid? manufacturerId = MakeMockManufacturer(seed);

            if (categoryId != null && manufacturerId != null) //todo bug creating more categories and manufacturers that needed
            {
                Guid? _ = MakeMockProduct((Guid)categoryId, (Guid)manufacturerId, seed); //return Guid is not used
                _logger.LogDebug("Mock data created");
            }
            else
            {
                _logger.LogError("Mock data creation error - check logs above!");
                return false;
            }
        }
        
        return true;
    }
}