using ECommerce.Domain.Models.Order;
using ECommerce.Services.BaseSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class OrderSpecification: BaseSpecifications<Order>
    {
        public OrderSpecification(string Email) : base(o => o.UserEmail == Email) 
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);

            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderSpecification(int Id) : base(o => o.Id == Id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);

        }
    }
}
