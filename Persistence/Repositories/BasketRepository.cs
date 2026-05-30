using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _connection = connection.GetDatabase();

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? span = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _connection.StringSetAsync(basket.Id, JsonBasket, span ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
            {
                return await GetCustomerBasketAsyn(basket.Id);
            }
            else
                return null;
        }

        public async Task<bool> DeleteCustomerBasketAsyn(string key)
        {
            return await _connection.KeyDeleteAsync(key);
        }

        public async Task<CustomerBasket?> GetCustomerBasketAsyn(string key)
        {
            var basket =await _connection.StringGetAsync(key);
            if (basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
