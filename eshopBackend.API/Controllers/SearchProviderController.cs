using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Services;
using eshopBackend.DAL;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchProviderController : ControllerBase
{
    private readonly ILogger<SearchProviderController> _logger;
    public SearchProviderController(ILogger<SearchProviderController> logger) => _logger = logger;


    [HttpGet("Category/{SearchTerm}")]
    public List<EntityCategory>? GetCategory(string searchTerm)
    {
        List<EntityCategory>? FoundCategory = DataAccessLayer.ServiceProvider.GetService<SearchProvider>()?.SearchCategoryByName(searchTerm);
        return FoundCategory;
    }

    [HttpGet("Manufacturer/{SearchTerm}")]
    public List<EntityManufacturer>? GetManufacturer(string searchTerm)
    {
        List<EntityManufacturer>? FoundManufacturer = DataAccessLayer.ServiceProvider.GetService<SearchProvider>()?.SearchManufacturerByName(searchTerm);
        return FoundManufacturer;
    }

    [HttpGet("Product/{SearchTerm}")]
    public List<EntityProduct>? GetProduct(string searchTerm)
    {
        List<EntityProduct>? FoundProduct = DataAccessLayer.ServiceProvider.GetService<SearchProvider>()?.SearchProductByName(searchTerm);
        return FoundProduct;
    }


}