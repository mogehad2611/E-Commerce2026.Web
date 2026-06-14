using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase connection = connection.GetDatabase();

        public async Task<string?> GetAsync(string CacheKey)
        {
            var cachevalue = await connection.StringGetAsync(CacheKey);
            return cachevalue.IsNullOrEmpty ? null : cachevalue.ToString();
        }

        public async Task SetASync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
            await connection.StringSetAsync(CacheKey, CacheValue, TimeToLive);
        }
    }
}
