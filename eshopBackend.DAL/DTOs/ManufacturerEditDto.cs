using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public class ManufacturerEditDto
    {
        string? Name { get; init; }
        
        string? Description { get; init; }
        
        string? LogoUrl { get; init; }

        string? Origin { get ; init; }
    }
}
