using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshopBackend.DAL.DTOs
{
    public record CartEditDTO
    {
        public Guid CartId { get; init; }
        
        public int? DeliveryType { get; init; }
        
        public string DeliveryAddress { get; init; }
        
        public int? PaymentType { get; init; }

        public string PaymentDetails { get; init; }
    }
}
