using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public record AddCategoryDto
    {
        public string Name { get; init; }
        
        public string? ImageUrl { get; init; }
        
        public string? Description { get; init; }
    }
}
