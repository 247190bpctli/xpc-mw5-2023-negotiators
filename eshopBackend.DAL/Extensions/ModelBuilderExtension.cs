using Microsoft.EntityFrameworkCore;
using Bogus;
using eshopBackend.DAL.Entities;

namespace eshopBackend.DAL.Extensions;

public static class ModelBuilderExtension
{
    public static void Seed(this ModelBuilder modelBuilder, int dataAmount)
    {
        List<CategoryEntity> categories = new();
        List<ManufacturerEntity> manufacturers = new();
        List<ProductEntity> products = new();

        for (int i = 0; i < dataAmount; i++)
        {
            CategoryEntity category = SeedCategory();
            ManufacturerEntity manufacturer = SeedManufacturer();
            ProductEntity product = SeedProduct(category, manufacturer);
            
            categories.Add(category);
            manufacturers.Add(manufacturer);
            products.Add(product);
        }

        modelBuilder.Entity<CategoryEntity>().HasData(categories);
        modelBuilder.Entity<ManufacturerEntity>().HasData(manufacturers);
        modelBuilder.Entity<ProductEntity>().HasData(products);
    }

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