using ECommerce.Domain.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Repository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string Key);
        Task<CustomerBasket> CreateUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLife = null);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
