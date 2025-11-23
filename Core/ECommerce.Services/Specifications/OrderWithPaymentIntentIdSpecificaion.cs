using ECommerce.Domain.Models.Order;
using ECommerce.Services.BaseSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public class OrderWithPaymentIntentIdSpecificaion:BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentIdSpecificaion(string IntentId)
            :base(o => o.PaymentIntentId == IntentId)
        {
            
        }
    }
}
