using eshopBackend.DAL.Entities;
using eshopBackend.DAL;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using eshopBackend.DAL.DTOs;
using System;

namespace eshopBackend.API.Controllers
{
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
                ManufacturerEntity details = _manufacturerRepository.ManufacturerDetails(id);
                return Ok(details);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Manufacturer cannot be found: {ExceptionMsg}", ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving manufacturer details: {ExceptionMsg}", ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("add/")]
        public ActionResult<Guid?> AddManufacturer(AddManufacturerDto addManufacturerDto)
        {
            try
            {
                return Ok(_manufacturerRepository.ManufacturerAdd(addManufacturerDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new manufacturer: {ExceptionMsg}", ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("edit/")]
        public ActionResult EditManufacturer([FromBody] EditManufacturerDto editManufacturerEditdto)
        {
            try
            {
                _manufacturerRepository.ManufacturerEdit(editManufacturerEditdto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing a manufacturer: {ExceptionMsg}", ex.Message);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a manufacturer: {ExceptionMsg}", ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("search/{searchTerm}")]
        public ActionResult<List<ManufacturerEntity>> SearchManufacturer(string searchTerm)
        {
            try
            {
                List<ManufacturerEntity> foundManufacturer = _manufacturerRepository.SearchManufacturerByName(searchTerm);
                return Ok(foundManufacturer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for manufacturers: {ExceptionMsg}", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
