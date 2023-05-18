using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL;

public class DbMigrator
{
    private readonly AppDbContext _db;
    private readonly ILogger<DbMigrator> _logger;

    public DbMigrator(AppDbContext db, ILogger<DbMigrator> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public void Migrate()
    {
        _logger.LogInformation("Startup migration started");
        _db.Database.Migrate();
        _logger.LogInformation("Startup migration finished");
    }
}