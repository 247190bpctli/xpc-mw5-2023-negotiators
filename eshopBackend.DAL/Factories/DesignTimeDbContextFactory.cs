using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eshopBackend.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        //take connection string directly from config
        IConfigurationBuilder builder =
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName)
                .AddJsonFile("eshopBackend.API/appsettings.json")
                .AddJsonFile("eshopBackend.API/appsettings.development.json", optional: true)
                .AddUserSecrets("f262d98a-8a24-4152-9d98-90fea8960d4c");

        IConfigurationRoot config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection") ?? string.Empty;

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Could not find connection string");
        }

        DbContextOptionsBuilder<AppDbContext> options = new();

        options.UseMySQL(connectionString);
        
        return new AppDbContext(options.Options, config);
    }
}