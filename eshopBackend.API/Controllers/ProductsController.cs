using eshopBackend.DAL;
using eshopBackend.DAL.DTOs;
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
        public ActionResult<List<ProductOverviewDto>> GetProducts(uint page)
        {
            List<ProductOverviewDto> products = _productRepository.ProductsOverview(page);

            return Ok(products);
        }

        [HttpGet("details/{id}")]
        public ActionResult<ProductDetailsDto> GetProductDetails(Guid id)
        {
            try
            {
                ProductDetailsDto details = _productRepository.ProductDetails(id);

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
            Guid productId = _productRepository.ProductAdd(productDto);

            return Ok(productId);
        }

        [HttpPut("edit/{id}")]
        public ActionResult EditProduct(Guid id, [FromBody] ProductEditDto productDto)
        {
            try
            {
                bool result = _productRepository.ProductEdit(id, productDto);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
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
                bool result = _productRepository.ProductDelete(id);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
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
            bool result = _productRepository.ReviewAdd(reviewDto);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
