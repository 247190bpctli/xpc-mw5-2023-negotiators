using System.Text.Json;
using eshopBackend.DAL;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LoggerFactory = eshopBackend.DAL.Factories.LoggerFactory;

namespace eshopBackend.App;

class Program
{
    public static void Main(string[] args)
    {
        DataAccessLayer dataAccessLayer = new();

        DataAccessLayer.serviceProvider.GetRequiredService<LoggerFactory>().Log.LogDebug("Hello!");
        
        //service access below [test]
        //DataAccessLayer.serviceProvider.GetRequiredService<MockDataGenerator>().MakeMockData(3);
        //DataAccessLayer.serviceProvider.GetRequiredService<CreateRecordTest>().ViewTest();
        
        //List<EntityProduct> pr = DataAccessLayer.serviceProvider.GetRequiredService<Products>().ProductsOverview(1);
        EntityProduct pr = DataAccessLayer.serviceProvider.GetRequiredService<Products>().ProductDetails(Guid.Parse("396820f9-3259-4906-a06c-a4654bf6bf59"));
        Console.WriteLine(JsonSerializer.Serialize(pr));
        //todo entities to guids
        
        //todo test services
        
        
        //config to log
        //serviceProvider.GetRequiredService<ConfigFactory>().LogConfigDebugView();
        
        DataAccessLayer.serviceProvider.GetRequiredService<LoggerFactory>().Log.LogDebug("Bye!");
    }
}
