using eshopBackend.DAL;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Services;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        public ProductsController(ILogger<ProductsController> logger) => _logger = logger;


        [HttpGet("/list/{page}")]
        public List<EntityProduct>? Get(byte page)
        {
            List<EntityProduct>? products = DataAccessLayer.serviceProvider?.GetService<Products>()?.ProductsOverview(page);
            return products;// new List<EntityProduct>();
        }

        [HttpGet("/detail/{id}")]
        public EntityProduct? Get(Guid id)
        {
            try
            {
                EntityProduct? details = DataAccessLayer.serviceProvider?.GetService<Products>()?.ProductDetails(id);
                return details;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Product cannot be found: {ExceptionMsg}", ex.Message);
                _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

                return null;
            }
        }

        [HttpPost("/add/{name},{imageUrl},{description},{price},{weight},{stock},{categoryId},{manufacturerId}")]
        public Guid? Post(string name, string? imageUrl, string? description, double price, double weight, int stock, Guid? categoryId, Guid? manufacturerId)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Products>().ProductAdd(name, imageUrl, description, price, weight, stock, categoryId, manufacturerId);
        }

        [HttpPut("/edit/{id},{name},{imageUrl},{description},{price},{weight},{stock},{categoryId},{manufacturerId}")]
        public bool Put(Guid id, string? name, string? imageUrl, string? description, double? price, double? weight, int? stock, Guid? categoryId, Guid? manufacturerId)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Products>().ProductEdit(id, name, imageUrl, description, price, weight, stock, categoryId, manufacturerId);
        }

        [HttpDelete("/delete/{id}")]
        public bool Delete(Guid id)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Products>().ProductDelete(id);
        }

        [HttpPost("/Review/{productId},{stars},{user},{description})")]
        public bool Post(Guid productId, byte stars, string user, string? description)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Products>().ReviewAdd(productId, stars, user, description);
        }
    }
}
