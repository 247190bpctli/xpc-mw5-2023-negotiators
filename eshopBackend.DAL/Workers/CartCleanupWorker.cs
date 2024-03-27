using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eshopBackend.DAL.Workers;

public class CartCleanupWorker : BackgroundService
{
    private readonly AppDbContext _db;
    private readonly TimeSpan _interval;
    private readonly ILogger<CartCleanupWorker> _logger;
    private readonly int _maxage;

    public CartCleanupWorker(ILogger<CartCleanupWorker> logger, IConfiguration config)
    {
        _db = new AppDbContext(new DbContextOptions<AppDbContext>(), config);
        _logger = logger;

        if (config.GetSection("Cart").GetValue<int>("RemovalInterval") != default && config.GetSection("Cart").GetValue<int>("MaxAge") != default)
        {
            _interval = TimeSpan.FromMinutes(config.GetSection("Cart").GetValue<int>("RemovalInterval"));
            _maxage = config.GetSection("Cart").GetValue<int>("MaxAge");
        }
        else
        {
            _logger.LogWarning("Cart removal interval and/or max age unset");
            _interval = TimeSpan.FromMinutes(5);
            _maxage = 1;
        }
    }

    public override void Dispose()
    {
        _db.Dispose();
        base.Dispose();
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        //startup message
        _logger.LogDebug("Cart removal worker started with an interval of {Interval} and max age of {MaxAge} hours", _interval, _maxage);
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested) 
        {
            DoWork();
            await Task.Delay(_interval, cancellationToken); 
        }
    }

    private void DoWork()
    {
        List<CartEntity> oldCarts = _db.Carts
            .Where(c => c.LastEdit < DateTime.Now.AddHours(-_maxage))
            .Where(c => c.Finalized == false)
            .ToList();

        if (oldCarts.Count != 0)
        {
            foreach (CartEntity cart in oldCarts) _db.Carts.Remove(cart);
        }

        _db.SaveChangesAsync();
        _logger.LogInformation("Removed {Amount} carts older than {Timeframe} hours", oldCarts.Count, _maxage);
    }
}