using DomainLayer.Models.OrderModule;
using Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string email);

        // Get Delivery Methods
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();

        // Get All Orders
        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email);

        // Get Order By Id
        Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id);
    }
}
