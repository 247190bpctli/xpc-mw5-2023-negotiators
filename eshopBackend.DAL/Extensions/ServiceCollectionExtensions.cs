using eshopBackend.DAL.Repositories;
using eshopBackend.DAL.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace eshopBackend.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterDalDependencies(this IServiceCollection services)
    {
        //DAL services
        services.AddDbContext<AppDbContext>();
        services.AddHostedService<CartCleanupWorker>();

        //public functions
        services.AddTransient<CartRepository>();
        services.AddTransient<CategoryRepository>();
        services.AddTransient<ManufacturerRepository>();
        services.AddTransient<ProductRepository>();
    }
}