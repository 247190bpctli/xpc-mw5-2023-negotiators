using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL.Factories;

public class DbConnectorFactory : DbContext
{
    public DbSet<EntityCart> Carts { get; set; }
    public DbSet<EntityPlacedOrder> PlacedOrders { get; set; }
    public DbSet<EntityCategory> Categories { get; set; }
    public DbSet<EntityManufacturer> Manufacturers { get; set; }
    public DbSet<EntityProduct> Products { get; set; }
    public DbSet<EntityReview> Reviews { get; set; }

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