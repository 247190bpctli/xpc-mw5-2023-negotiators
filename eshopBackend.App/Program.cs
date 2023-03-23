using eshopBackend.DAL;
using eshopBackend.DAL.DbSettings;

namespace eshopBackend.App;

class Program
{
    public static void Main(string[] args)
    {
        DataAccessLayer dataAccessLayer = new();

        //dataAccessLayer.Command<DbReset>();
        
        Console.WriteLine("Hello, World!");
    }
}
