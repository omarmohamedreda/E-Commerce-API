using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models.Order
{
    public enum OrderStatus
    {
        pending = 0,
        PaymentReceived = 1,
        PaymentFailed = 2,
    }
}
