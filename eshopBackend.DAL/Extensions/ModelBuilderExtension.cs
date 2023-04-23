using Microsoft.EntityFrameworkCore;
using Bogus;
using eshopBackend.DAL.Entities;

namespace eshopBackend.DAL.Extensions;

public static class ModelBuilderExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>()
            .HasData(
                SeedCategory(),
                SeedCategory(),
                SeedCategory()
            );
        
        modelBuilder.Entity<ManufacturerEntity>()
            .HasData(
                SeedManufacturer(),
                SeedManufacturer(),
                SeedManufacturer()
            );
        
        modelBuilder.Entity<ManufacturerEntity>()
            .HasData(
                SeedProduct(category1, manufacturer1),
                SeedProduct(category2, manufacturer2),
                SeedProduct(category3, manufacturer3)
            );
    }
    
    /*
     * for (int i = 0; i < dataAmount; i++)
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
     */
    
    private static CategoryEntity SeedCategory()
    {
        return new Faker<CategoryEntity>()
            .RuleFor(o => o.Name, f => f.Vehicle.Type())
            .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
            .RuleFor(o => o.Description, f => f.Lorem.Lines(1));
    }

    private static ManufacturerEntity SeedManufacturer()
    {
        return new Faker<ManufacturerEntity>()
            .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
            .RuleFor(o => o.Description, f => f.Lorem.Lines(1))
            .RuleFor(o => o.LogoUrl, f => f.Image.DataUri(200, 100))
            .RuleFor(o => o.Origin, f => f.Address.Country());
    }
    
    private static ProductEntity SeedProduct(CategoryEntity category, ManufacturerEntity manufacturer)
    {
        ProductEntity seededProduct = new Faker<ProductEntity>()
            .RuleFor(o => o.Name, f => f.Vehicle.Manufacturer())
            .RuleFor(o => o.ImageUrl, f => f.Image.DataUri(200, 100))
            .RuleFor(o => o.Description, f => f.Lorem.Lines(1))
            .RuleFor(o => o.Price, f => f.Random.Number(499))
            .RuleFor(o => o.Weight, f => f.Random.Number(60))
            .RuleFor(o => o.Stock, f => f.Random.Number(5));

        seededProduct.Category = category;
        seededProduct.Manufacturer = manufacturer;
        seededProduct.Reviews = new List<ReviewEntity>();
        
        return seededProduct;
    }
}