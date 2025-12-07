using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Basket;
using ECommerce.Shared.DTOS.BasketDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class BasketServices(IBasketRepository _repository, IMapper _mapper) : IBasketServices
    {
        public async Task<BasketDto> CreateOrUpdateUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var SaveBasket = await _repository.CreateUpdateBasketAsync(CustomerBasket);
            if (SaveBasket is not null)
                return await GetBasketAsync(SaveBasket.Id);
            else
                throw new Exception("Failed to save basket");
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _repository.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await _repository.GetBasketAsync(Key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(Key);
        }
    }
}
