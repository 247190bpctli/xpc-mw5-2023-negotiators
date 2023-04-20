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

    //MIGRATION in-code connection string
    public AppDbContext(){}
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseMySQL("");
    }
    //ENDS MIGRATION in-code connection string

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}