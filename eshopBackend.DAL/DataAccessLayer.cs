using eshopBackend.DAL.Factories;
using eshopBackend.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LoggerFactory = eshopBackend.DAL.Factories.LoggerFactory;

namespace eshopBackend.DAL;

public class DataAccessLayer
{
    public static ServiceProvider serviceProvider;
    
    public DataAccessLayer()
    {
        //build dependency services
        var serviceCollection = new ServiceCollection();
        
        //DAL services
        serviceCollection.AddSingleton<ConfigFactory>();
        serviceCollection.AddSingleton<LoggerFactory>();
        serviceCollection.AddDbContext<DbConnectorFactory>(options =>
        {
            options.UseMySQL(serviceProvider.GetRequiredService<ConfigFactory>().GetFirstConnectionString());
        });
        
        //public functions
        serviceCollection.AddTransient<Cart>();
        serviceCollection.AddTransient<Categories>();
        serviceCollection.AddTransient<Manufacturers>();
        serviceCollection.AddTransient<MockDataGenerator>();
        serviceCollection.AddTransient<Products>();
        serviceCollection.AddTransient<SearchProvider>();

        // Build ServiceProvider - any registrations after this line will not take effect 
        serviceProvider = serviceCollection.BuildServiceProvider();
        
        //scopes
        /*var serviceScopeProvider = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using (var scope = serviceScopeProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<Service>();
        }*/
    }
}
