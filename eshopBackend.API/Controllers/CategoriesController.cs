using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using eshopBackend.DAL.DTOs;

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

    [HttpPost("add/")]
    public ActionResult<Guid> AddCategory(AddCategoryDto addCategoryDto)
    {
        Guid CategoryId = _categoryRepository.CategoryAdd(addCategoryDto);
        return Ok(CategoryId); 
        
    }

    [HttpPut("edit/")]
    public ActionResult EditCategory(EditCategoryDto editCategoryDto)
    {
        _categoryRepository.CategoryEdit(editCategoryDto);
        return Ok(); 
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteCategory(Guid id)
    {
        _categoryRepository.CategoryDelete(id);
        return Ok();
    }

    [HttpGet("search/{searchTerm}")]
    public ActionResult<List<CategoryEntity>?> GetCategory(string searchTerm)
    {
        List<CategoryEntity>? FoundCategory = _categoryRepository.SearchCategoryByName(searchTerm);
        return Ok(FoundCategory);
    }
}
