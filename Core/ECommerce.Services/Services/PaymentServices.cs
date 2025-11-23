using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Basket;
using ECommerce.Domain.Models.Order;
using ECommerce.Domain.Models.Product;
using ECommerce.Shared.DTOS.BasketDto_s;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = ECommerce.Domain.Models.Product.Product;

namespace ECommerce.Services.Services
{
    public class PaymentServices(IConfiguration _configuration, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentServices
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            // Configuration Stripe : Install package Stripe.net
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            // GetBasket from repository
            var Basket = await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId) ;
            // Get Amount, product, Delivery method Price
            var ProductRepo = _unitOfWork.GetRepository<Product>();
            foreach (var item in Basket.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                item.Price = product.Price;
               
            }

            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);

            Basket.ShippingPrice = DeliveryMethod.Price;
            var BasketAmount = (long)(Basket.Items.Sum(i => i.Quantity * i.Price) + DeliveryMethod.Price) * 100 ;

            // Create or Update Payment Intent with Stripe API

            var PaymentMethod = new PaymentIntentService();
            // Create Payment Intent
            if (Basket.PaymentIntentId is null)
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentintent = await PaymentMethod.CreateAsync(options);
                Basket.PaymentIntentId = paymentintent.Id;
                Basket.ClientSecret = paymentintent.ClientSecret;
            }

            else 
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = BasketAmount,
                };
                await PaymentMethod.UpdateAsync(Basket.PaymentIntentId, options);
            }

            await _basketRepository.CreateUpdateBasketAsync(Basket);
            return _mapper.Map<CustomerBasket, BasketDto>(Basket);

        }
    }
}
