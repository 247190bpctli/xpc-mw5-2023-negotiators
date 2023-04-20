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
    public ProductsController(ILogger<ProductsController> logger) => _logger = logger;


    [HttpGet("list/{page}")]
    public List<EntityProduct>? GetProducts(byte page)
    {
        List<EntityProduct>? products = DataAccessLayer.ServiceProvider.GetService<Products>()?.ProductsOverview(page);
        return products;
    }

    [HttpGet("details/{id}")]
    public EntityProduct? GetProductDetails(Guid id)
    {
        try
        {
            EntityProduct? details = DataAccessLayer.ServiceProvider.GetService<Products>()?.ProductDetails(id);
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
        return DataAccessLayer.ServiceProvider.GetRequiredService<Products>().ProductAdd(name, imageUrl, description, price, weight, stock, categoryId, manufacturerId);
    }

    [HttpPut("edit/{id}/{name}/{imageUrl}/{description}/{price}/{weight}/{stock}/{categoryId}/{manufacturerId}")]
    public bool EditProduct(Guid id, string? name, string? imageUrl, string? description, double? price, double? weight, int? stock, Guid? categoryId, Guid? manufacturerId)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Products>().ProductEdit(id, name, imageUrl, description, price, weight, stock, categoryId, manufacturerId);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteProduct(Guid id)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Products>().ProductDelete(id);
    }

    [HttpPost("Review/{productId}/{stars}/{user}/{description})")]
    public bool AddReview(Guid productId, byte stars, string user, string? description)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Products>().ReviewAdd(productId, stars, user, description);
    }
}