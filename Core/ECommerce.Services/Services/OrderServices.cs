using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Order;
using ECommerce.Domain.Models.Product;
using ECommerce.Services.Specifications;
using ECommerce.Shared.DTOS.AuthenticationDto_s;
using ECommerce.Shared.DTOS.OrderDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class OrderServices(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            // Map Address to Order Address
            var OrderAddress = _mapper.Map<AddressDto,OrderAddress>(orderDto.Address);

            // Get Basket
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);


            ArgumentNullException.ThrowIfNull(Basket.PaymentIntentId);
            var OrderRebo = _unitOfWork.GetRepository<Order>();
            var Specification = new OrderWithPaymentIntentIdSpecificaion(Basket.PaymentIntentId);
            var ExistingOrder = await OrderRebo.GetByIdWihSpecificationsAsync(Specification);
            if (ExistingOrder is not null) OrderRebo.Delete(ExistingOrder);

            // Create OrderItems List
            List<OrderItem> OrderItems = [];

            var ProductRepo = _unitOfWork.GetRepository<Product>();
            foreach (var item in Basket.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                OrderItems.Add(orderItem);
            }

            // Get Delivery Method  
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId) ;


            // Calculate Subtotal
            var Subtotal = OrderItems.Sum(item => item.Price * item.Quantity);

            // Create Order
            var order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, Subtotal, Basket.PaymentIntentId);

            await _unitOfWork.GetRepository<Order>().AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Specification = new OrderSpecification(Email);
            var OrderRepo = await _unitOfWork.GetRepository<Order>().GetAllWihSpecificationsAsync(Specification);

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(OrderRepo);

        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethodRepo = await _unitOfWork.GetRepository<DeliveryMethod>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethodRepo);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(int orderId)
        {
            var Specification = new OrderSpecification(orderId);
            var OrderRepo = await  _unitOfWork.GetRepository<Order>().GetByIdWihSpecificationsAsync(Specification);
            return _mapper.Map<Order, OrderToReturnDto>(OrderRepo);
        }
    }
}
