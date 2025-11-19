using ECommerce.Shared.DTOS.AuthenticationDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DTOS.OrderDto_s
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto Address { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public decimal Total { get; set; }
    }
}
