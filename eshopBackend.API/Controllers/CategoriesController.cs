using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly CategoryRepository _categoryRepository;

    public CategoriesController(ILogger<CategoriesController> logger, CategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }


    [HttpGet("list/{page}")]
    public List<CategoryEntity>? GetCategories(uint page)
    {
        List<CategoryEntity>? categories = _categoryRepository.CategoriesOverview(page);
        return categories;
    }

    [HttpGet("details/{id}")]
    public CategoryEntity? GetCategoryDetails(Guid id)
    {
        try
        {
            CategoryEntity? category = _categoryRepository.CategoryDetails(id);
            return category;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Category cannot be found: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

            return null;
        }
    }

    [HttpPost("add/{name}/{imageUrl}/{description}")]
    public Guid? AddCategory(string name, string? imageUrl, string? description)
    {
        return _categoryRepository.CategoryAdd(name, imageUrl, description);
    }

    [HttpPut("edit/{id}/{name}/{imageUrl}/{description}")]
    public bool EditCategory(Guid id, string? name, string? imageUrl, string? description)
    {
        return _categoryRepository.CategoryEdit(id, name, imageUrl, description);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteCategory(Guid id)
    {
        return _categoryRepository.CategoryDelete(id);
    }
}
