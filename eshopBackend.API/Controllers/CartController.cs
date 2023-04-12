﻿using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Services;
using eshopBackend.DAL;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;
    public CartController(ILogger<CartController> logger) => _logger = logger;


    [HttpGet("detail/{id}")]
    public EntityCart? Get(Guid id)
    {
        try
        {
            EntityCart? details = DataAccessLayer.ServiceProvider.GetService<Cart>()?.CartDetails(id);
            return details;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Product cannot be found: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

            return null;
        }
    }

    [HttpGet("Create/")]
    public Guid? Create()
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Cart>().CartAdd();
    }

    [HttpPut("edit/{cartId},{deliveryType},{deliveryAddress},{paymentType},{paymentDetails}")]
    public bool Put(Guid cartId, int? deliveryType, string? deliveryAddress, int? paymentType, string? paymentDetails)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Cart>().CartEdit(cartId, deliveryType, deliveryAddress, paymentType, paymentDetails);
    }

    [HttpDelete("delete/{id}")]
    public bool Delete(Guid id)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Cart>().CartDelete(id);
    }

    [HttpPatch("AddToCart/{cartId},{productId},{amount}")]
    public bool AddToCart(Guid cartId, Guid productId, int amount)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Cart>().AddToCart(cartId, productId, amount);
    }

    [HttpGet("finalizeOrder/{cartId}")]
    public bool FinalizeOrder(Guid cartId)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Cart>().FinalizeOrder(cartId);
    }

}