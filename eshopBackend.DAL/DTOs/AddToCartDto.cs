using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public record AddToCartDTO
    {
        public Guid CartId { get; init; }

        public Guid ProductId { get; init; }
        
        public int Amount { get; init; }
    }
}
