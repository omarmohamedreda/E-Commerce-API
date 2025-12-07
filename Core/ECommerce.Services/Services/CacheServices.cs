using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class CacheServices(ICacheRepository _cacheRepository) : ICacheServices
    {
        public async Task<string?> GetAsync(string Chachekey)
        {
           return await _cacheRepository.GetAsync(Chachekey);
        }

        public async Task SetAsync(string key, object cacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue);  // Serialize the object to a JSON string

            await _cacheRepository.SetAsync(key, value, TimeToLive);
        }
    }
}
