using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DTOs;
using Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string email)
        {
            var OrderAddress = mapper.Map<AddressDTO, OrderAddress>(orderDTO.Address);

            var Basket = await basketRepository.GetCustomerBasketAsyn(orderDTO.BasketId) 
                ?? throw new BasketNotFoundException(orderDTO.BasketId);

            List<OrderItem> orderItems = [];
            var ProductRepo = unitOfWork.GetRepository<Product, int>();

            foreach(var item in Basket.Items)
            {
                var Product = ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                var ProductItemOrdered =
                    new ProductItemOrdered(Product.Result!.Id, Product.Result.PictureUrl, Product.Result.Name);

                var OrderItem =
                    new OrderItem(item.Quantity, Product.Result.Price, ProductItemOrdered);

                orderItems.Add(OrderItem);
            }

            var DeliveryMethod =
                await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDTO.DeliveryMethod) 
                ?? throw new DeliveryMethodNotFoundException(orderDTO.DeliveryMethod);

            var Subtotal = orderItems.Sum(I => I.Quantity * I.Price);

            var Order = new Order(email, OrderAddress, DeliveryMethod, Subtotal, orderItems);

            await unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Order, OrderToReturnDTO>(Order);
        }


        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);

            var Orders = await unitOfWork
                .GetRepository<Order, Guid>()
                .GetAllAsync(specifications: Spec);

            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(Orders);
        }
        

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await unitOfWork
                .GetRepository<DeliveryMethod, int>()
                .GetAllAsync();

            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id)
        {
            var Spec = new OrderSpecifications(Id);

            var Order = await unitOfWork
                .GetRepository<Order, Guid>()
                .GetByIdAsync(specifications: Spec);

            return mapper.Map<Order, OrderToReturnDTO>(Order);
        }
    }
}
