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
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "Category with ID '{ID}' not found", id);
            return NotFound();
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
            return CreatedAtAction(nameof(GetCategoryDetails), new { id = categoryId }, categoryId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting category details");
            return StatusCode(500);
        }
    }

    [HttpPut("edit/{id}")]
    public ActionResult EditCategory(Guid id, [FromBody] EditCategoryDto editCategoryDto)
    {
        try
        {
            _categoryRepository.CategoryEdit(id, editCategoryDto);
            return CreatedAtAction(nameof(GetCategoryDetails), new { Id = id }, id);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while editing Category {id}", id);
            return NotFound();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while editing Category {id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while editing Category {id}", id);
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
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "Tried delete category with ID '{ID}', Not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a category");
            return StatusCode(500);
        }
    }

    [HttpGet("search/{searchTerm}")]
    public ActionResult<List<CategoryEntity>?> GetCategory(string searchTerm)
    {
        try
        {
            return Ok(_categoryRepository.SearchCategoryByName(searchTerm));
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while searching for category: {ExceptionMsg}", ex.Message);
            return StatusCode(500);
        }
    }

}
