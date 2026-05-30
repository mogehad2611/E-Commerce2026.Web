using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager serviceManager): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string key)
        {
            var basket = await serviceManager.BasketService.GetBasket(key);
            return Ok(basket);
        }


        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basketDTO)
        {
            var basket = await serviceManager.BasketService.CreateOrUpdateBasket(basketDTO);
            return Ok(basket);
        }



        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await serviceManager.BasketService.DeleteBasket(key);
            return Ok(result);
        }
    }
}
