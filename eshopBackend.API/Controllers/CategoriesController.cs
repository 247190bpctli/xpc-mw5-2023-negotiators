﻿using eshopBackend.DAL.Entities;
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
    public List<EntityCategory>? GetCategories(byte page)
    {
        List<EntityCategory>? categories = DataAccessLayer.ServiceProvider?.GetService<Categories>()?.CategoriesOverview(page);
        return categories;
    }

    [HttpGet("details/{id}")]
    public EntityCategory? GetCategoryDetails(Guid id)
    {
        try
        {
            EntityCategory? category = DataAccessLayer.ServiceProvider?.GetService<Categories>()?.CategoryDetails(id);
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
        return DataAccessLayer.ServiceProvider.GetRequiredService<Categories>().CategoryAdd(name, imageUrl, description);
    }

    [HttpPut("edit/{id}/{name}/{imageUrl}/{description}")]
    public bool EditCategory(Guid id, string? name, string? imageUrl, string? description)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Categories>().CategoryEdit(id, name, imageUrl, description);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteCategory(Guid id)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Categories>().CategoryDelete(id);
    }
}
