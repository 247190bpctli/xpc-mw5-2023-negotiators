using eshopBackend.DAL.DbSettings;
using LinqToDB.Data;
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
        
        serviceCollection.AddSingleton<ConfigLoader>();
        serviceCollection.AddSingleton<Logger>();
        serviceCollection.AddSingleton<EshopBackendDb>();
        serviceCollection.AddTransient<DbReset>();
        
        // Build ServiceProvider - any registrations after this line will not take effect 
        serviceProvider = serviceCollection.BuildServiceProvider();
        
        //load default db settings
        DataConnection.DefaultSettings = new DbSettings.DbSettings(serviceProvider.GetRequiredService<ConfigLoader>().GetFirstConnectionString());
        
        //scopes
        /*var serviceScopeProvider = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using (var scope = serviceScopeProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<Service>();
        }*/
        
        serviceProvider.GetRequiredService<ConfigLoader>().LogConfigDebugView();
        
        serviceProvider.GetRequiredService<Logger>().Log.LogTrace("Hello, DAL!");
    }
}
