using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class CartCleanupService : IHostedService, IDisposable
{
    private readonly AppDbContext _db;
    private readonly CartRepository _cartRepository;
    private Timer _timer;

    public CartCleanupService(AppDbContext db, CartRepository cartRepository)
    {
        _db = db;
        _cartRepository = cartRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async state => await RemoveOldCartsAsync(), null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
        return Task.CompletedTask;
    }

    private async Task RemoveOldCartsAsync()
    {
        // najdeme všechny košíky s LastEdit starší než 24 hodin
        List<CartEntity> oldCarts = await _db.Carts
            .Where(c => c.LastEdit < DateTime.UtcNow.AddMinutes(-1))
            .ToListAsync();

        if (oldCarts.Count == 0)
        {
            return;
        }

        // smažeme každý nalezený košík
        foreach (var cart in oldCarts)
        {
            _cartRepository.CartDelete(cart.Id);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // zastavíme timer
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // zrušíme timer
        _timer?.Dispose();
    }
}
