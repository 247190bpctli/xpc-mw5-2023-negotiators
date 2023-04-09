using eshopBackend.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LoggerFactory = eshopBackend.DAL.LoggerFactory;

namespace eshopBackend.App;

class Program
{
    public static void Main(string[] args)
    {
        DataAccessLayer dataAccessLayer = new();

        DataAccessLayer.serviceProvider.GetRequiredService<LoggerFactory>().Log.LogDebug("Hello!");
        
        //service access below [test]
        //DataAccessLayer.serviceProvider.GetRequiredService<CreateRecordTest>().CreateTest();
        //DataAccessLayer.serviceProvider.GetRequiredService<CreateRecordTest>().ViewTest();
        //todo test services
        
        //config to log
        //serviceProvider.GetRequiredService<ConfigFactory>().LogConfigDebugView();
        
        DataAccessLayer.serviceProvider.GetRequiredService<LoggerFactory>().Log.LogDebug("Bye!");
    }
}
