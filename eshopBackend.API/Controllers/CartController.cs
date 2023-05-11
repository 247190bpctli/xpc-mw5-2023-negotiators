using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
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
    public ActionResult<CartEntity> GetCartDetails(Guid id)
    {
        try
        {
            CartEntity details = _cartRepository.CartDetails(id) ?? throw new InvalidOperationException("Trying to show details of cart null");
            return Ok(details);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of cart: {Id}", id);
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of cart: {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of cart: {Id}", id);
            return StatusCode(500);
        }
    }

    [HttpPost("create")]
    public ActionResult<Guid> CreateCart()
    {
        try
        {
            Guid cartId = _cartRepository.CartAdd();
            return CreatedAtAction(nameof(GetCartDetails), new { id = cartId }, cartId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating new cart");
            return StatusCode(500);
        }
    }

    [HttpPut("edit/{id}")]
    public ActionResult EditCart(Guid id, [FromBody] EditCartDto editCartDto)
    {
        try
        {
            _cartRepository.CartEdit(id, editCartDto);
            return CreatedAtAction(nameof(GetCartDetails), new { Id = id }, id);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while editing cart {Id}", id);
            return NotFound();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while editing cart {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while editing cart {Id}", id);
            return StatusCode(500);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteCart(Guid id)
    {
        try
        {
            _cartRepository.CartDelete(id);
            return Ok();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "Tried delete cart with ID '{Id}', Not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a cart");
            return StatusCode(500);
        }
    }

    [HttpPost("AddToCart/{id}")]
    public ActionResult AddToCart(Guid id, [FromBody] AddToCartDto addToCartDto)
    {
        try
        {
            _cartRepository.AddToCart(id, addToCartDto);
            return CreatedAtAction(nameof(GetCartDetails), new { Id = id }, id);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while Adding product to cart:{Id}", id);
            return NotFound();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while Adding product to cart:{Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Adding product to cart:{Id}", id);
            return StatusCode(500);
        }
    }

    [HttpGet("finalizeOrder/{id}")]
    public ActionResult FinalizeOrder(Guid id)
    {
        try
        {
            _cartRepository.FinalizeOrder(id);
            return CreatedAtAction(nameof(GetCartDetails), new { Id = id }, id);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while editing product {Id}", id);
            return StatusCode(500);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while editing product {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while editing product {Id}", id);
            return StatusCode(500);
        }
    }
}