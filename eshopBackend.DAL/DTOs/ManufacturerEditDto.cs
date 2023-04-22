using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public class ManufacturerEditDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; init; }
        
        public string? Description { get; init; }
        
        public string? LogoUrl { get; init; }
        
        public string? Origin { get ; init; }
    }
}
