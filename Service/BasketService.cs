using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository repository,IMapper mapper) : IBasketService
    {
        public Task<BasketDTO> CreateOrUpdateBasket(BasketDTO basket)
        {
            var CustomerBasket = mapper.Map<BasketDTO, CustomerBasket>(basket);
            var IsCreated = repository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (IsCreated is not null)
            {
                return GetBasket(basket.Id);
            }
            else
                throw new Exception("Basket Does not exist");
        }

        public async Task<bool> DeleteBasket(string key)
        {
            return await repository.DeleteCustomerBasketAsyn(key);
        }

        public async Task<BasketDTO> GetBasket(string key)
        {
            var Basket = await repository.GetCustomerBasketAsyn(key);
            if (Basket is not null)
            {
                return mapper.Map<CustomerBasket, BasketDTO>(Basket);
            }
            else
                throw new BasketNotFoundException(key);
        }
    }
}
