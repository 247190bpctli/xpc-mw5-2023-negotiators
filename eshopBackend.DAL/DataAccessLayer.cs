using eshopBackend.DAL.DbSettings;
using LinqToDB.Data;
using Microsoft.Extensions.DependencyInjection;


namespace eshopBackend.DAL;

public class DataAccessLayer
{
    private static ServiceProvider serviceProvider;
    
    public DataAccessLayer()
    {
        //load default db settings
        DataConnection.DefaultSettings = new DbSettings.DbSettings();
        
        //build dependency services
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<EshopBackendDb>();
        serviceCollection.AddTransient<DbReset>();
        
        // Build ServiceProvider - any registrations after this line will not take effect 
        serviceProvider = serviceCollection.BuildServiceProvider();
        
        //scopes
        /*var serviceScopeProvider = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using (var scope = serviceScopeProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<Service>();
        }*/
        
        Console.WriteLine("Hello, DAL!");
    }

    public T Command<T>() where T : notnull
    {
        return serviceProvider.GetRequiredService<T>();
    }
}
