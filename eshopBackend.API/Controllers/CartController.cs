using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using eshopBackend.DAL.DTOs;

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
    public ActionResult<CartEntity> GetCartDetails(Guid id)
    {
        CartEntity? details = _cartRepository.CartDetails(id);
        return Ok(details);

    }

    [HttpPost("Create/")]
    public ActionResult<Guid> CreateCart()
    {
        return Ok(_cartRepository.CartAdd());
    }

    [HttpPut("edit/")]
    public ActionResult EditCart(EditCartDto editCartDto)
    {
        _cartRepository.CartEdit(editCartDto);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteCart(Guid id)
    {
        _cartRepository.CartDelete(id);
        return Ok();
    }

    [HttpPost("AddToCart/")]
    public ActionResult AddToCart(AddToCartDto addToCartDto)
    {
        _cartRepository.AddToCart(addToCartDto);
        return Ok();
    }

    [HttpGet("finalizeOrder/{cartId}")]
    public ActionResult FinalizeOrder(Guid cartId)
    {
        _cartRepository.FinalizeOrder(cartId);
        return Ok();
    }
}