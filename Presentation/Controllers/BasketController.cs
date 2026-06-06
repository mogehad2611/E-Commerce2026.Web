using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.BasketModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager serviceManager): APIBaseController
    {
        [HttpGet("{key}")]
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
