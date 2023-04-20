using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL;

public class AppDbContext : DbContext
{
    public DbSet<CartEntity> Carts { get; set; } = null!;
    public DbSet<PlacedOrderEntity> PlacedOrders { get; set; } = null!;
    public DbSet<CategoryEntity> Categories { get; set; } = null!;
    public DbSet<ManufacturerEntity> Manufacturers { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<ReviewEntity> Reviews { get; set; } = null!;

    //MIGRATION in-code connection string [comment out if not migrating]
    /*public AppDbContext(){}
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseMySQL(connectionString);
    }*/
    //ENDS MIGRATION

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}