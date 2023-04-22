using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public record ReviewAddDto
    {
        public Guid ProductId { get; init; }

        public double Stars { get; set; }

        public string User { get; init; }

        public string? Description { get; init; }
    }
}
