using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public class ProductOverviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }

    public class ProductDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public int Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ManufacturerId { get; set; }
    }

    public class ProductAddDto
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public int Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ManufacturerId { get; set; }
    }

    public class ProductEditDto
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public double? Weight { get; set; }
        public int? Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ManufacturerId { get; set; }
    }

    public class ReviewAddDto
    {
        public Guid ProductId { get; set; }
        public double Stars { get; set; }
        public string User { get; set; }
        public string? Description { get; set; }
    }
}