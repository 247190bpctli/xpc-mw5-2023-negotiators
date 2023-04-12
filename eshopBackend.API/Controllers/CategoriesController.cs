using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Services;
using eshopBackend.DAL;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    public CategoriesController(ILogger<CategoriesController> logger) => _logger = logger;


    [HttpGet("list/{page}")]
    public List<EntityCategory>? Get(byte page)
    {
        List<EntityCategory>? category = DataAccessLayer.serviceProvider?.GetService<Categories>()?.CategoriesOverview(page);
        return category;
    }

    [HttpGet("detail/{id}")]
    public EntityCategory? Get(Guid id)
    {
        try
        {
            EntityCategory? details = DataAccessLayer.serviceProvider?.GetService<Categories>()?.CategoryDetails(id);
            return details;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Category cannot be found: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

            return null;
        }
    }

    [HttpPost("add/{name},{imageUrl},{description}")]
    public Guid? Post(string name, string? imageUrl, string? description)
    {
        return DataAccessLayer.serviceProvider.GetRequiredService<Categories>().CategoryAdd(name, imageUrl, description);
    }

    [HttpPut("edit/{id},{name},{imageUrl},{description}")]
    public bool Put(Guid id, string? name, string? imageUrl, string? description)
    {
        return DataAccessLayer.serviceProvider.GetRequiredService<Categories>().CategoryEdit(id, name, imageUrl, description);
    }

    [HttpDelete("delete/{id}")]
    public bool Delete(Guid id)
    {
        return DataAccessLayer.serviceProvider.GetRequiredService<Categories>().CategoryDelete(id);
    }
}