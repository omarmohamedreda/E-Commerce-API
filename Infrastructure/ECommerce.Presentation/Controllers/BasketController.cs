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
    [Authorize]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var basket = await _serviceManager.BasketServices.GetBasketAsync(Key);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateUpdateBasket(BasketDto basket)
        {
            var Internalbasket = await _serviceManager.BasketServices.CreateOrUpdateUpdateBasketAsync(basket);
            return Ok(Internalbasket);
        }

        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var result = await _serviceManager.BasketServices.DeleteBasketAsync(Key);
            return Ok(result);
        }

    }
}
