using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL;

public class DataAccessLayer
{
    public static ServiceProvider serviceProvider;
    
    public DataAccessLayer()
    {
        //build dependency services
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddSingleton<ConfigFactory>();
        serviceCollection.AddSingleton<LoggerFactory>();
        serviceCollection.AddDbContext<DbConnectorFactory>(options =>
        {
            options.UseMySQL(serviceProvider.GetRequiredService<ConfigFactory>().GetFirstConnectionString());
        });
        serviceCollection.AddTransient<CreateRecordTest>();

        // Build ServiceProvider - any registrations after this line will not take effect 
        serviceProvider = serviceCollection.BuildServiceProvider();
        
        //scopes
        /*var serviceScopeProvider = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using (var scope = serviceScopeProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<Service>();
        }*/
        
        serviceProvider.GetRequiredService<ConfigFactory>().LogConfigDebugView();
        
        serviceProvider.GetRequiredService<LoggerFactory>().Log.LogTrace("Hello, DAL!");
    }
}
