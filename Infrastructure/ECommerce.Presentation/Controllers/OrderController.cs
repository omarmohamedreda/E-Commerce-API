using ECommerce.Abstraction;
using ECommerce.Shared.DTOS.OrderDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
                var Email = User.FindFirstValue(ClaimTypes.Email);
                var Order = await _serviceManager.OrderServices.CreateOrderAsync(orderDto,Email);
                return Ok(Order);
        }

        
        [HttpGet("DeliverMehtod")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetDeliveryMethods()
        {
                var deliveryMethods = await _serviceManager.OrderServices.GetDeliveryMethodsAsync();
                return Ok(deliveryMethods);
        }


        [HttpGet("AllOrders")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrdersForUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _serviceManager.OrderServices.GetAllOrdersAsync(Email);
            return Ok(Orders);
        }

        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int OrderId)
        {
            var Orders = await _serviceManager.OrderServices.GetOrderByIdAsync(OrderId);
            return Ok(Orders);
        }

    }
      
            
}
