using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL;

public class CreateRecordTest
{
    private readonly DbConnectorFactory _db;

    public CreateRecordTest(DbConnectorFactory db)
    {
        _db = db;
    }

    public void CreateTest()
    {
        //add row to db test
        EntityProduct test = new()
        {
            Name = "Kokot Jumbo",
            Price = (float)59.99,
            Weight = (float)5.501,
            Stock = 56,
            Id = default
        };

        DbSet<EntityProduct> products = _db.Set<EntityProduct>();

        products.Add(test);
        _db.SaveChanges();
    }
}