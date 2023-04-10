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
        
        //bogus strict mode - all properties have to be defined
        Faker.DefaultStrictMode = true;
    }

    public bool MakeMockData(byte categoryAmount, byte manufacturerAmount, byte productAmount, int? seed)
    {
        //todo bogus
        return false;
    }

    /*public int? MakeMockCategory(byte amount, int? seed)
    {
        try
        {
            //make seed if not specified
            int actualSeed = seed ?? new Random().Next(99999);

            Randomizer.Seed = new Random(actualSeed);
        
            for(byte i = 0; i < amount; i++)
            {
                EntityCategory bogusCategory = new Faker<EntityCategory>()
                    .RuleFor(o => o.Id, f => f.Random.Guid())
                    .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
                    .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
                    .RuleFor(o => o.Description, f => f.Lorem.Lines(1));

                _db.Categories.Add(bogusCategory);
            }

            _db.SaveChanges();

            return actualSeed;
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
    
    public int? MakeMockManufacturer(byte amount, int? seed)
    {
        try
        {
            //make seed if not specified
            int actualSeed = seed ?? new Random().Next(99999);

            Randomizer.Seed = new Random(actualSeed);
        
            for(byte i = 0; i < amount; i++)
            {
                EntityManufacturer bogusManufacturer = new Faker<EntityManufacturer>()
                    .RuleFor(o => o.Id, f => f.Random.Guid()) //todo props
                    .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
                    .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
                    .RuleFor(o => o.Description, f => f.Lorem.Lines(1));

                _db.Manufacturers.Add(bogusManufacturer);
            }

            _db.SaveChanges();

            return actualSeed;
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
    
    public int? MakeMockProduct(byte amount, int? seed)
    {
        try
        {
            //make seed if not specified
            int actualSeed = seed ?? new Random().Next(99999);

            Randomizer.Seed = new Random(actualSeed);
        
            for(byte i = 0; i < amount; i++)
            {
                EntityProduct bogusProduct = new Faker<EntityProduct>()
                    .RuleFor(o => o.Id, f => f.Random.Guid()) //todo props
                    .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
                    .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
                    .RuleFor(o => o.Description, f => f.Lorem.Lines(1));

                _db.Products.Add(bogusProduct);
            }

            _db.SaveChanges();

            return actualSeed;
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
    }*/
}