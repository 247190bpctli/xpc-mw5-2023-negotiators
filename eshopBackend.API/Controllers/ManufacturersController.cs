using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Services;
using eshopBackend.DAL;
using Microsoft.AspNetCore.Mvc;



namespace eshopBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {

        private readonly ILogger<ManufacturersController> _logger;
        public ManufacturersController(ILogger<ManufacturersController> logger) => _logger = logger;


        [HttpGet("list/{page}")]
        public List<EntityManufacturer>? Get(byte page)
        {
            List<EntityManufacturer>? manufacturer = DataAccessLayer.serviceProvider?.GetService<Manufacturers>()?.ManufacturersOverview(page);
            return manufacturer;
        }

        [HttpGet("detail/{id}")]
        public EntityManufacturer? Get(Guid id)
        {
            try
            {
                EntityManufacturer? details = DataAccessLayer.serviceProvider?.GetService<Manufacturers>()?.ManufacturerDetails(id);
                return details;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Manufacturer cannot be found: {ExceptionMsg}", ex.Message);
                _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

                return null;
            }
        }

        [HttpPost("add/{name},{description},{logoUrl},{origin}")]
        public Guid? Post(string name, string? description, string? logoUrl, string? origin)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Manufacturers>().ManufacturerAdd(name, description, logoUrl, origin);
        }

        [HttpPut("edit/{id},{name},{description},{logoUrl},{origin}")]
        public bool Put(Guid id, string? name, string? description, string? logoUrl, string? origin)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Manufacturers>().ManufacturerEdit(id, name, description, logoUrl, origin);
        }

        [HttpDelete("delete/{id}")]
        public bool Delete(Guid id)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Manufacturers>().ManufacturerDelete(id);
        }

    }
}
