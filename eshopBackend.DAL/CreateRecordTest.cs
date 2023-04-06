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

    private static readonly EntityProduct _test = new()
    {
        Id = Guid.NewGuid(),
        Name = "Kokot Jumbo",
        Price = (float)59.99,
        Weight = (float)5.501,
        Stock = 56,
        Reviews = Array.Empty<EntityReview>()
    };
    
    public void CreateTest()
    {
        //add row to db test
        DbSet<EntityProduct> products = _db.Set<EntityProduct>();

        products.Add(_test);
        _db.SaveChanges();
    }
    
    public void ViewTest()
    {
        List<EntityProduct> products = _db.Products.ToList();
        Console.WriteLine(products);
    }
}