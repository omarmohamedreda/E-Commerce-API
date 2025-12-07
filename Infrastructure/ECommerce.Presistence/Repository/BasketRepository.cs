using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Models.Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Repository
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _redisDatabase = connection.GetDatabase();
        public async Task<CustomerBasket> CreateUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _redisDatabase.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromHours(5));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(basket.Id); 
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _redisDatabase.KeyDeleteAsync(Key);
        }

        public async Task<CustomerBasket> GetBasketAsync(string Key)
        {
            var Basket = await _redisDatabase.StringGetAsync(Key);
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }
    }
}
