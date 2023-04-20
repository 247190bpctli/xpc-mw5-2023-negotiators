using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManufacturersController : ControllerBase
{

    private readonly ILogger<ManufacturersController> _logger;
    public ManufacturersController(ILogger<ManufacturersController> logger) => _logger = logger;


    [HttpGet("list/{page}")]
    public List<ManufacturerEntity>? GetManufacturers(byte page)
    {
        List<ManufacturerEntity>? manufacturers = DataAccessLayer.ServiceProvider?.GetService<Manufacturers>()?.ManufacturersOverview(page);
        return manufacturers;
    }

    [HttpGet("details/{id}")]
    public ManufacturerEntity? GetManufacturerDetails(Guid id)
    {
        try
        {
            ManufacturerEntity? details = DataAccessLayer.ServiceProvider?.GetService<Manufacturers>()?.ManufacturerDetails(id);
            return details;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Manufacturer cannot be found: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

            return null;
        }
    }

    [HttpPost("add/{name}/{description}/{logoUrl}/{origin}")]
    public Guid? AddManufacturer(string name, string? description, string? logoUrl, string? origin)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Manufacturers>().ManufacturerAdd(name, description, logoUrl, origin);
    }

    [HttpPut("edit/{id}/{name}/{description}/{logoUrl}/{origin}")]
    public bool EditManufacturer(Guid id, string? name, string? description, string? logoUrl, string? origin)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Manufacturers>().ManufacturerEdit(id, name, description, logoUrl, origin);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteManufacturer(Guid id)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<Manufacturers>().ManufacturerDelete(id);
    }

}
