using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL;

public class DbConnector : DbContext
{
    public DbSet<EntityCategory> Categories { get; set; }
    public DbSet<EntityManufacturer> Manufacturers { get; set; }
    public DbSet<EntityProduct> Products { get; set; }
    public DbSet<EntityReview> Reviews { get; set; }

    //MIGRATION in-code connection string [comment out if not migrating]
    /*public DbConnector(){}
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseMySQL(connectionString);
    }*/
    //ENDS MIGRATION

    public DbConnector(DbContextOptions<DbConnector> options) : base(options)
    {
    }
}