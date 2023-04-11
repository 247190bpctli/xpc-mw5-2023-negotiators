using eshopBackend.DAL;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eshopBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {


        //list products
        [HttpGet("/list/{page}")]
        public List<EntityProduct>? Get(byte page)
        {
            List<EntityProduct>? products = DataAccessLayer.serviceProvider?.GetService<Products>()?.ProductsOverview(page);
            return products;// new List<EntityProduct>();
        }


        //product detail
        [HttpGet("/detail/{id}")]
        public EntityProduct? Get(string id)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Products>().ProductDetails(Guid.Parse(id));
        }

        //product add
        [HttpPost("/add/{name},{imageUrl},{description},{price},{weight},{stock},{categoryId},{manufacturerId}")]
        public Guid? Post(string name, string? imageUrl, string? description, double price, double weight, int stock, Guid? categoryId, Guid? manufacturerId)
        {
            return DataAccessLayer.serviceProvider.GetRequiredService<Products>().ProductAdd(name, imageUrl, description, price, weight, stock, categoryId, manufacturerId);
        }

        //// PUT api/<ProductsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
