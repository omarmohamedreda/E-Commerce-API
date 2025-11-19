using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models.Order
{
    public class OrderItem:BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductItemOrder Product { get; set; } 
    }
}
