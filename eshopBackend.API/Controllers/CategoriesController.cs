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
    public ActionResult<List<CategoryEntity>> GetCategories(uint page)
    {
        try
        {
            List<CategoryEntity> categories = _categoryRepository.CategoriesOverview(page);
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting Category overview");
            return StatusCode(500);
        }
    }

    [HttpGet("details/{id}")]
    public ActionResult<CategoryEntity?> GetCategoryDetails(Guid id)
    {
        try
        {
            CategoryEntity details = _categoryRepository.CategoryDetails(id);
            return Ok(details);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Category with ID '{ID}' not found", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting Catogory details");
            return StatusCode(500);
        }
    }

    [HttpPost("add/")]
    public ActionResult<Guid> AddCategory(AddCategoryDto addCategoryDto)
    {
        try
        {
            Guid categoryId = _categoryRepository.CategoryAdd(addCategoryDto);
            return Ok(categoryId); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting product details");
            return StatusCode(500);
        }
    }

    [HttpPut("edit/")]
    public ActionResult EditCategory(EditCategoryDto editCategoryDto)
    {
        try
        {
            _categoryRepository.CategoryEdit(editCategoryDto);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Category cannot be edited: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteCategory(Guid id)
    {
        try
        {
            _categoryRepository.CategoryDelete(id);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Category cannot be deleted: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting Category with ID {id}",id);
            return StatusCode(500);
        }
    }

    [HttpGet("search/{searchTerm}")]
    public ActionResult<List<CategoryEntity>?> GetCategory(string searchTerm)
    {
        try
        {
            List<CategoryEntity> foundCategory = _categoryRepository.SearchCategoryByName(searchTerm);
            return Ok(foundCategory);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Category search failed: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);
            return StatusCode(500);
        }
    }

}
