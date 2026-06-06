using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) : APIBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var Email = GetEmailFromToken();
            var Order = await serviceManager.OrderService.CreateOrder(orderDTO, Email);
            return Ok(Order);
        }
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")] 
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await serviceManager.OrderService.GetDeliveryMethodsAsync();

            return Ok(DeliveryMethods);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders()
        {
            var Orders = await serviceManager.OrderService
                .GetAllOrdersAsync(Email: GetEmailFromToken());

            return Ok(Orders);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id)
        {
            var Order = await serviceManager.OrderService.GetOrderByIdAsync(Id: id);

            return Ok(Order);
        }
    }
}
