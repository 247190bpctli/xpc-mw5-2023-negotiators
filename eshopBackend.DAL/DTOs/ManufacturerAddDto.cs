using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public record ManufacturerAddDto
    {
        public string Name { get; init; }

        public string? Description { get; init; }

        public string? LogoUrl { get; init; }

        public string? Origin { get; init; }

    }
}
