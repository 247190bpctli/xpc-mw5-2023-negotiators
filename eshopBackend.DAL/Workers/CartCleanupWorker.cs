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

    public CartCleanupWorker(IConfiguration config, ILogger<CartCleanupWorker> logger)
    {
        _db = new AppDbContext(new DbContextOptions<AppDbContext>(), config);
        _logger = logger;

        try
        {
            _interval = TimeSpan.FromHours(config.GetSection("Cart").GetValue<int>("RemovalInterval"));
            _maxage = config.GetSection("Cart").GetValue<int>("MaxAge");
        }
        catch (NullReferenceException ex)
        {
            _logger.LogWarning("Cart removal interval and/or max age unset");
            _logger.LogWarning("{ExceptionMessage}", ex.Message);
            _interval = TimeSpan.FromHours(1);
            _maxage = 1;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_interval);

        while (!stoppingToken.IsCancellationRequested
               && await timer.WaitForNextTickAsync(stoppingToken))
        {
            List<CartEntity> oldCarts = _db.Carts
                .Where(c => c.LastEdit < DateTime.UtcNow.AddHours(_maxage))
                .Where(c => c.Finalized == false)
                .ToList();

            if (oldCarts.Count != 0)
            {
                foreach (CartEntity cart in oldCarts) _db.Carts.Remove(cart);

                await _db.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Removed {Amount} carts older than {Timeframe} hours", oldCarts.Count, _maxage);
            }
        }
    }
}