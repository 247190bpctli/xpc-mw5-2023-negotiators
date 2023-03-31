using eshopBackend.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eshopBackend.App;

class Program
{
    public static void Main(string[] args)
    {
        DataAccessLayer dataAccessLayer = new();

        DataAccessLayer.serviceProvider.GetRequiredService<Logger>().Log.LogDebug("Hello!");
        
        //service access below [test]
        //DataAccessLayer.serviceProvider.GetRequiredService<DbReset>();
        
        DataAccessLayer.serviceProvider.GetRequiredService<Logger>().Log.LogDebug("Bye!");
    }
}
