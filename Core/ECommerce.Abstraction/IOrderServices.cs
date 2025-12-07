using ECommerce.Shared.DTOS.OrderDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction
{
    public interface IOrderServices
    {
        // Create Order
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email);

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);

        Task<OrderToReturnDto> GetOrderByIdAsync(int orderId);



    }
}
