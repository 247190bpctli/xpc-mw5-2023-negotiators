using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchProviderController : ControllerBase
{
    private readonly SearchProvider _searchProvider;

    public SearchProviderController(SearchProvider searchProvider,
        ManufacturerRepository manufacturerRepository, ProductRepository productRepository)
    {
        _searchProvider = searchProvider;
    }
    
    [HttpGet("Category/{SearchTerm}")]
    public List<CategoryEntity>? GetCategory(string searchTerm)
    {
        List<CategoryEntity>? FoundCategory = _searchProvider.SearchCategoryByName(searchTerm);
        return FoundCategory;
    }

    [HttpGet("Manufacturer/{SearchTerm}")]
    public List<ManufacturerEntity>? GetManufacturer(string searchTerm)
    {
        List<ManufacturerEntity>? FoundManufacturer = _searchProvider.SearchManufacturerByName(searchTerm);
        return FoundManufacturer;
    }

    [HttpGet("Product/{SearchTerm}")]
    public List<ProductEntity>? GetProduct(string searchTerm)
    {
        List<ProductEntity>? FoundProduct = _searchProvider.SearchProductByName(searchTerm);
        return FoundProduct;
    }


}