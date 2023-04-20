using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;
    public CartController(ILogger<CartController> logger) => _logger = logger;


    [HttpGet("details/{id}")]
    public CartEntity? GetCartDetails(Guid id)
    {
        try
        {
            CartEntity? details = DataAccessLayer.ServiceProvider.GetService<CartRepository>()?.CartDetails(id);
            return details;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Cart details cannot be found: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

            return null;
        }
    }

    [HttpGet("Create/")]
    public Guid? CreateCart()
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<CartRepository>().CartAdd();
    }

    [HttpPut("edit/{cartId}/{deliveryType}/{deliveryAddress}/{paymentType}/{paymentDetails}")]
    public bool Put(Guid cartId, int? deliveryType, string? deliveryAddress, int? paymentType, string? paymentDetails)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<CartRepository>().CartEdit(cartId, deliveryType, deliveryAddress, paymentType, paymentDetails);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteCart(Guid id)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<CartRepository>().CartDelete(id);
    }

    [HttpPatch("AddToCart/{cartId}/{productId}/{amount}")]
    public bool AddToCart(Guid cartId, Guid productId, int amount)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<CartRepository>().AddToCart(cartId, productId, amount);
    }

    [HttpGet("finalizeOrder/{cartId}")]
    public bool FinalizeOrder(Guid cartId)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<CartRepository>().FinalizeOrder(cartId);
    }
}