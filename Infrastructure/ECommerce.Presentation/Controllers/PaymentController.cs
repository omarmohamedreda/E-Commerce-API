using ECommerce.Abstraction;
using ECommerce.Shared.DTOS.BasketDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IServiceManager _serviceManager): ControllerBase
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var Basket = await _serviceManager.paymentServices.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(Basket);
        }
    }
}
