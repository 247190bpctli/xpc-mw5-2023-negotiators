using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL;

public class AppDbContext : DbContext
{
    private readonly bool _seedDemoData;
    public DbSet<CartEntity> Carts { get; set; } = null!;
    public DbSet<PlacedOrderEntity> PlacedOrders { get; set; } = null!;
    public DbSet<CategoryEntity> Categories { get; set; } = null!;
    public DbSet<ManufacturerEntity> Manufacturers { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<ProductInCartEntity> ProductsInCart { get; set; } = null!;
    public DbSet<ReviewEntity> Reviews { get; set; } = null!;

    //MIGRATION in-code connection string [comment out if not migrating]
    /*public AppDbContext() {}
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseMySQL("");
    }*/
    //ENDS MIGRATION in-code connection string

    public AppDbContext(DbContextOptions<AppDbContext> options, bool seedDemoData = false) : base(options)
    {
        _seedDemoData = seedDemoData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        if (_seedDemoData)
        {
            modelBuilder.Seed(5);
        }
    }
}