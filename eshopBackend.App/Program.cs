using eshopBackend.DAL.DbSettings;
using eshopBackend.DAL.Entities;
using LinqToDB;
using LinqToDB.Data;

namespace eshopBackend.App;

class Program
{
    public static void Main(string[] args)
    {
        DataConnection.DefaultSettings = new DbSettings();

        Console.WriteLine("Hello, World!");


        using (var db = new EshopBackendDb("eshopBackendDB"))
            //using (var db = new DbMapper())
        {
            /*var q =
                from c in db.Products
                select c;

            foreach (var c in q)
                Console.WriteLine(c.ContactName);*/

            try
            {
                db.CreateTable<EntityProduct>();
                db.CreateTable<EntityCategory>();
                db.CreateTable<EntityRating>();
                db.CreateTable<EntityVendor>();
            }
            catch(MySqlConnector.MySqlException){}

            
        }
    }
}
