using eshopBackend.DAL;
using eshopBackend.DAL.DbSettings;
using Microsoft.Extensions.DependencyInjection;

namespace eshopBackend.App;

class Program
{
    public static void Main(string[] args)
    {
        DataAccessLayer dataAccessLayer = new();

        //DataAccessLayer.serviceProvider.GetRequiredService<DbReset>();
        
        Console.WriteLine("Hello, World!");
    }
}
