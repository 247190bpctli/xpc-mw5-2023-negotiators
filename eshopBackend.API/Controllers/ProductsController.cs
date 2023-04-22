using eshopBackend.DAL;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using eshopBackend.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace eshopBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductRepository _productRepository;

        public ProductsController(ILogger<ProductsController> logger, ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("list/{page}")]
        public ActionResult<List<ProductEntity>> GetProducts(uint page)
        {
            List<ProductEntity> products = _productRepository.ProductsOverview(page);

            return Ok(products);
        }

        [HttpGet("details/{id}")]
        public ActionResult<ProductEntity> GetProductDetails(Guid id)
        {
            try
            {
                ProductEntity details = _productRepository.ProductDetails(id);

                return Ok(details);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Product cannot be found: {ExceptionMsg}", ex.Message);
                _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

                return NotFound();
            }
        }

        [HttpPost("add")]
        public ActionResult<Guid> AddProduct([FromBody] ProductAddDto productDto)
        {
            Guid? productId = _productRepository.ProductAdd(productDto);

            return Ok(productId);
        }

        [HttpPut("edit/{id}")]
        public ActionResult EditProduct([FromBody] ProductEditDto productDto)
        {
            try
            {
                _productRepository.ProductEdit(productDto);
                
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Product cannot be found: {ExceptionMsg}", ex.Message);
                _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

                return NotFound();
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            try
            {
                _productRepository.ProductDelete(id);
                 return Ok();            
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Product cannot be found: {ExceptionMsg}", ex.Message);
                _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);

                return NotFound();
            }
        }

        [HttpPost("review")]
        public ActionResult AddReview([FromBody] ReviewAddDto reviewDto)
        {
            _productRepository.ReviewAdd(reviewDto);
            return Ok();   
        }

        [HttpGet("search/{searchTerm}")]
        public ActionResult<List<ProductEntity>> GetProduct(string searchTerm)
        {
            List<ProductEntity>? FoundProduct = _productRepository.SearchProductByName(searchTerm);
            return Ok(FoundProduct);
        }
    }
}
