using eshopBackend.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eshopBackend.IntegrationTests;

public class TestDbContext : AppDbContext
{
    public TestDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options, config)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDBContext");
    }
}