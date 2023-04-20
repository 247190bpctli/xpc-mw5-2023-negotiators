using eshopBackend.DAL.Factories;
using eshopBackend.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL;

public class DataAccessLayer
{
    public static ServiceProvider ServiceProvider = null!;
    
    public DataAccessLayer()
    {
        //build dependency services
        var serviceCollection = new ServiceCollection();
        
        //DAL services
        serviceCollection.AddSingleton<ConfigFactory>();
        serviceCollection.AddLogging(b => b //todo take levels from global config
            #if DEBUG
                .AddConsole().SetMinimumLevel(LogLevel.Debug)
            #else
                .AddConsole().SetMinimumLevel(LogLevel.Warning)
            #endif
        );
        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySQL(ServiceProvider.GetRequiredService<ConfigFactory>().GetFirstConnectionString());
        });
        
        //public functions
        serviceCollection.AddTransient<Cart>();
        serviceCollection.AddTransient<Categories>();
        serviceCollection.AddTransient<Manufacturers>();
        serviceCollection.AddTransient<MockDataGenerator>();
        serviceCollection.AddTransient<Products>();
        serviceCollection.AddTransient<SearchProvider>();

        // Build ServiceProvider - any registrations after this line will not take effect 
        ServiceProvider = serviceCollection.BuildServiceProvider();
        
        //startup message
        ServiceProvider.GetService<ILogger<DataAccessLayer>>()!.LogDebug("DAL instance created");
        
        //scopes
        /*var serviceScopeProvider = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using (var scope = serviceScopeProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<Service>();
        }*/
    }
}
