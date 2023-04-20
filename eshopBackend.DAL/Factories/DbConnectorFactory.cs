using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL.Factories;

public class DbConnectorFactory : DbContext
{
    public DbSet<CartEntity> Carts { get; set; }
    public DbSet<PlacedOrderEntity> PlacedOrders { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ManufacturerEntity> Manufacturers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }

    //MIGRATION in-code connection string [comment out if not migrating]
    /*public DbConnectorFactory(){}
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseMySQL(connectionString);
    }*/
    //ENDS MIGRATION

    public DbConnectorFactory(DbContextOptions<DbConnectorFactory> options) : base(options)
    {
    }
}