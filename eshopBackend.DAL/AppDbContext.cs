using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eshopBackend.DAL;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    public DbSet<CartEntity> Carts { get; set; } = null!;
    public DbSet<CategoryEntity> Categories { get; set; } = null!;
    public DbSet<ManufacturerEntity> Manufacturers { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<ProductInCartEntity> ProductsInCart { get; set; } = null!;
    public DbSet<ReviewEntity> Reviews { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_config.GetConnectionString("DefaultConnection") ?? string.Empty);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        if (_config.GetSection("Seeds").GetValue<bool>("SeedMockData"))
        {
            modelBuilder.Seed(_config.GetSection("Seeds").GetValue<uint>("DataAmount"));
        }
    }
}