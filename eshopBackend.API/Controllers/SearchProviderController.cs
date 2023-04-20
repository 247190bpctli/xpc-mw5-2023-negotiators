using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchProviderController : ControllerBase
{

    [HttpGet("Category/{SearchTerm}")]
    public List<CategoryEntity>? GetCategory(string searchTerm)
    {
        List<CategoryEntity>? FoundCategory = DataAccessLayer.ServiceProvider.GetService<SearchProvider>()?.SearchCategoryByName(searchTerm);
        return FoundCategory;
    }

    [HttpGet("Manufacturer/{SearchTerm}")]
    public List<ManufacturerEntity>? GetManufacturer(string searchTerm)
    {
        List<ManufacturerEntity>? FoundManufacturer = DataAccessLayer.ServiceProvider.GetService<SearchProvider>()?.SearchManufacturerByName(searchTerm);
        return FoundManufacturer;
    }

    [HttpGet("Product/{SearchTerm}")]
    public List<ProductEntity>? GetProduct(string searchTerm)
    {
        List<ProductEntity>? FoundProduct = DataAccessLayer.ServiceProvider.GetService<SearchProvider>()?.SearchProductByName(searchTerm);
        return FoundProduct;
    }


}