using eshopBackend.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eshopBackend.DAL.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterDalDependencies(this IServiceCollection services, IConfiguration config)
    {
        //DAL services
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySQL(config.GetConnectionString("DefaultConnection") ?? string.Empty);
        });
        
        //public functions
        services.AddTransient<CartRepository>();
        services.AddTransient<CategoryRepository>();
        services.AddTransient<ManufacturerRepository>();
        services.AddTransient<MockDataGenerator>();
        services.AddTransient<ProductRepository>();
        services.AddTransient<SearchProvider>();
    }
}