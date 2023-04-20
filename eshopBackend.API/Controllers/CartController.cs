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
    private readonly CartRepository _cartRepository;

    public CartController(ILogger<CartController> logger, CartRepository cartRepository)
    {
        _logger = logger;
        _cartRepository = cartRepository;
    } 


    [HttpGet("details/{id}")]
    public CartEntity? GetCartDetails(Guid id)
    {
        try
        {
            CartEntity? details = _cartRepository.CartDetails(id);
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
        return _cartRepository.CartAdd();
    }

    [HttpPut("edit/{cartId}/{deliveryType}/{deliveryAddress}/{paymentType}/{paymentDetails}")]
    public bool Put(Guid cartId, int? deliveryType, string? deliveryAddress, int? paymentType, string? paymentDetails)
    {
        return _cartRepository.CartEdit(cartId, deliveryType, deliveryAddress, paymentType, paymentDetails);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteCart(Guid id)
    {
        return _cartRepository.CartDelete(id);
    }

    [HttpPatch("AddToCart/{cartId}/{productId}/{amount}")]
    public bool AddToCart(Guid cartId, Guid productId, int amount)
    {
        return _cartRepository.AddToCart(cartId, productId, amount);
    }

    [HttpGet("finalizeOrder/{cartId}")]
    public bool FinalizeOrder(Guid cartId)
    {
        return _cartRepository.FinalizeOrder(cartId);
    }
}