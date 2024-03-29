using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eshopBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly ProductRepository _productRepository;

    public ProductsController(ILogger<ProductsController> logger, ProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet("list/{page}")]
    public ActionResult<List<ProductEntity>> GetProducts(uint page)
    {
        try
        {
            List<ProductEntity> products = _productRepository.ProductsOverview(page);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting products overview");
            return StatusCode(500);
        }
    }

    [HttpGet("details/{id}")]
    public ActionResult<ProductEntity> GetProductDetails(Guid id)
    {
        try
        {
            ProductEntity details = _productRepository.ProductDetails(id)! ?? throw new InvalidOperationException("Trying to show details of product null");
            return Ok(details);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of product: {Id}", id);
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of product: {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of product: {Id}", id);
            return StatusCode(500);
        }
    }

    [HttpPost("add")]
    public ActionResult<Guid> AddProduct([FromBody] ProductDto productDto)
    {
        try
        {
            Guid productId = _productRepository.ProductAdd(productDto);
            return CreatedAtAction(nameof(GetProductDetails), new { id = productId }, productId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a new product");
            return StatusCode(500);
        }
    }

    [HttpPut("edit/{id}")]
    public ActionResult EditProduct(Guid id, [FromBody] ProductDto productDto)
    {
        try
        {
            _productRepository.ProductEdit(id, productDto);
            return CreatedAtAction(nameof(GetProductDetails), new { Id = id }, id);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while editing product {Id}", id);
            return NotFound();
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

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteProduct(Guid id)
    {
        try
        {
            _productRepository.ProductDelete(id);
            return Ok();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "Tried delete product with ID '{ID}', Not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a product");
            return StatusCode(500);
        }
    }

    [HttpPost("review/{id}")]
    public ActionResult AddReview(Guid id, [FromBody] AddReviewDto addReviewDto)
    {
        try
        {
            _productRepository.ReviewAdd(id, addReviewDto);
            return CreatedAtAction(nameof(GetProductDetails), new { Id = id }, id);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while adding a review");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a review");
            return StatusCode(500);
        }
    }

    [HttpGet("search/{searchTerm}")]
    public ActionResult<List<ProductEntity>> SearchProductByName(string searchTerm)
    {
        try
        {
            return Ok(_productRepository.SearchProductByName(searchTerm));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while searching for products: {ExceptionMsg}", ex.Message);
            return StatusCode(500);
        }
    }
}