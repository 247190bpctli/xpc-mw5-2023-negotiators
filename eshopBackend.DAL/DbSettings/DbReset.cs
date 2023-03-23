using eshopBackend.DAL.Entities;
using LinqToDB;

namespace eshopBackend.DAL.DbSettings;

public class DbReset
{
    private readonly EshopBackendDb _db;
    
    public DbReset(EshopBackendDb db)
    {
        _db = db;
        
        try
        {
            //drop tables if any
            DropTableIfExists<EntityProduct>();
            DropTableIfExists<EntityCategory>();
            DropTableIfExists<EntityReview>();
            DropTableIfExists<EntityManufacturer>();

            //recreate tables
            db.CreateTable<EntityProduct>();
            db.CreateTable<EntityCategory>();
            db.CreateTable<EntityReview>();
            db.CreateTable<EntityManufacturer>();
        }
        catch (MySqlConnector.MySqlException e)
        {
            Console.WriteLine(e.Message);
            //Console.WriteLine(e.StackTrace);
        }
    }
    
    void DropTableIfExists<T>() where T : notnull
    {
        try {
           _db.BeginTransaction();
           _db.DropTable<T>();
           _db.CommitTransaction();
        }catch{
           _db.RollbackTransaction();
        }
    }
}