using eshopBackend.DAL;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public List<ProductEntity>? GetProducts(uint page)
    {
        List<ProductEntity>? products = _productRepository.ProductsOverview(page);
        return products;
    }

    [HttpGet("details/{id}")]
    public ProductEntity? GetProductDetails(Guid id)
    {
        try
        {
            ProductEntity? details = _productRepository.ProductDetails(id);
            return details;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Product cannot be found: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

            return null;
        }
    }

    [HttpPost("add/{name}/{imageUrl}/{description}/{price}/{weight}/{stock}/{categoryId}/{manufacturerId}")]
    public Guid? AddProduct(string name, string? imageUrl, string? description, double price, double weight, int stock, Guid? categoryId, Guid? manufacturerId)
    {
        return _productRepository.ProductAdd(name, imageUrl, description, price, weight, stock, categoryId, manufacturerId);
    }

    [HttpPut("edit/{id}/{name}/{imageUrl}/{description}/{price}/{weight}/{stock}/{categoryId}/{manufacturerId}")]
    public bool EditProduct(Guid id, string? name, string? imageUrl, string? description, double? price, double? weight, int? stock, Guid? categoryId, Guid? manufacturerId)
    {
        return _productRepository.ProductEdit(id, name, imageUrl, description, price, weight, stock, categoryId, manufacturerId);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteProduct(Guid id)
    {
        return _productRepository.ProductDelete(id);
    }

    [HttpPost("Review/{productId}/{stars}/{user}/{description})")]
    public bool AddReview(Guid productId, double stars, string user, string? description)
    {
        return _productRepository.ReviewAdd(productId, stars, user, description);
    }
}