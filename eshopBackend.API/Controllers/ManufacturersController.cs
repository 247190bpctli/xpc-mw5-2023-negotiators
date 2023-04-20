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
    private readonly ManufacturerRepository _manufacturerRepository;

    public ManufacturersController(ILogger<ManufacturersController> logger, ManufacturerRepository manufacturerRepository)
    {
        _logger = logger;
        _manufacturerRepository = manufacturerRepository;
    }


    [HttpGet("list/{page}")]
    public List<ManufacturerEntity>? GetManufacturers(uint page)
    {
        List<ManufacturerEntity>? manufacturers = _manufacturerRepository.ManufacturersOverview(page);
        return manufacturers;
    }

    [HttpGet("details/{id}")]
    public ManufacturerEntity? GetManufacturerDetails(Guid id)
    {
        try
        {
            ManufacturerEntity? details = _manufacturerRepository.ManufacturerDetails(id);
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
        return _manufacturerRepository.ManufacturerAdd(name, description, logoUrl, origin);
    }

    [HttpPut("edit/{id}/{name}/{description}/{logoUrl}/{origin}")]
    public bool EditManufacturer(Guid id, string? name, string? description, string? logoUrl, string? origin)
    {
        return _manufacturerRepository.ManufacturerEdit(id, name, description, logoUrl, origin);
    }

    [HttpDelete("delete/{id}")]
    public bool DeleteManufacturer(Guid id)
    {
        return _manufacturerRepository.ManufacturerDelete(id);
    }

}
