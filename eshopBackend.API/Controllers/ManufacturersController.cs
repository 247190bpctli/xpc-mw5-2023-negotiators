using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using eshopBackend.DAL.DTOs;

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
    public ActionResult<List<ManufacturerEntity>> GetManufacturers(uint page)
    {
        List<ManufacturerEntity> manufacturers = _manufacturerRepository.ManufacturersOverview(page);
        return Ok(manufacturers);
    }

    [HttpGet("details/{id}")]
    public ActionResult<ManufacturerEntity> GetManufacturerDetails(Guid id)
    {
        try
        {
            ManufacturerEntity? details = _manufacturerRepository.ManufacturerDetails(id);
            return Ok(details);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Manufacturer cannot be found: {ExceptionMsg}", ex.Message);
            _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

            return NotFound();
        }
    }

    [HttpPost("add/")]
    public ActionResult<Guid?> AddManufacturer(ManufacturerAddDto manufacturerAddDto)
    {
        return Ok(_manufacturerRepository.ManufacturerAdd(manufacturerAddDto));
    }

    [HttpPut("edit/")]
    public ActionResult EditManufacturer([FromBody]ManufacturerEditDto manufacturerEditdto)
    {
        _manufacturerRepository.ManufacturerEdit(manufacturerEditdto);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteManufacturer(Guid id)
    {
        _manufacturerRepository.ManufacturerDelete(id);
        return Ok();
    }

    [HttpGet("search/{searchTerm}")]
    public ActionResult<List<ManufacturerEntity>?> SearchManufacturer(string searchTerm)
    {
        List<ManufacturerEntity>? FoundManufacturer = _manufacturerRepository.SearchManufacturerByName(searchTerm);
        return Ok(FoundManufacturer);
    }

}
