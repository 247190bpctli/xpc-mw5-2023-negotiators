using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
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
    public ActionResult<List<ManufacturerEntity>> GetManufacturers(uint page)
    {
        try
        {
            List<ManufacturerEntity> manufacturers = _manufacturerRepository.ManufacturersOverview(page);
            return Ok(manufacturers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the list of manufacturers");
            return StatusCode(500);
        }
    }

    [HttpGet("details/{id}")]
    public ActionResult<ManufacturerEntity> GetManufacturerDetails(Guid id)
    {
        try
        {
            ManufacturerEntity details = _manufacturerRepository.ManufacturerDetails(id) ?? throw new InvalidOperationException("Trying to show details of manufacturer null");
            return Ok(details);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of manufacturer: {Id}", id);
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of manufacturer: {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting details of manufacturer: {Id}", id);
            return StatusCode(500);
        }
    }

    [HttpPost("add")]
    public ActionResult<Guid?> AddManufacturer(ManufacturerDto manufacturerDto)
    {
        try
        {
            Guid manufacturerId = _manufacturerRepository.ManufacturerAdd(manufacturerDto);
            return CreatedAtAction(nameof(GetManufacturerDetails), new { id = manufacturerId }, manufacturerId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a new manufacturer: {ExceptionMsg}", ex.Message);
            return StatusCode(500);
        }
    }

    [HttpPut("edit/{id}")]
    public ActionResult EditManufacturer(Guid id, [FromBody] ManufacturerDto manufacturerDto)
    {
        try
        {
            _manufacturerRepository.ManufacturerEdit(id, manufacturerDto);
            return CreatedAtAction(nameof(GetManufacturerDetails), new { Id = id }, id);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "An error occurred while editing manufacturer {Id}", id);
            return NotFound();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "An error occurred while editing manufacturer {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while editing manufacturer {Id}", id);
            return StatusCode(500);
        }
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteManufacturer(Guid id)
    {
        try
        {
            _manufacturerRepository.ManufacturerDelete(id);
            return Ok();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "Tried delete manufacturer with ID '{ID}', Not found", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a manufacturer");
            return StatusCode(500);
        }
    }

    [HttpGet("search/{searchTerm}")]
    public ActionResult<List<ManufacturerEntity>> SearchManufacturer(string searchTerm)
    {
        try
        {
            return Ok(_manufacturerRepository.SearchManufacturerByName(searchTerm));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while searching for manufacturers: {ExceptionMsg}", ex.Message);
            return StatusCode(500);
        }
    }
}