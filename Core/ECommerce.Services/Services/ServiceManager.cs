using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _BasketRepository, UserManager<ApplicationUser> userManager, IConfiguration _configuration) : IServiceManager
    {

        #region Product
        private readonly Lazy<IProductServices> _LazyProductServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));

        public IProductServices ProductServices => _LazyProductServices.Value;
        #endregion

        #region Basket
        private readonly Lazy<IBasketServices> _LazyBasketServices = new Lazy<IBasketServices>(() => new BasketServices(_BasketRepository, _mapper));
        public IBasketServices BasketServices => _LazyBasketServices.Value;
        #endregion

        #region Authentication
        private readonly Lazy<IAuthenticationServices> _LazyAuthenticationService = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager, _configuration, _mapper));
        public IAuthenticationServices AuthenticationServices => _LazyAuthenticationService.Value;
        #endregion

        #region Order
        private readonly Lazy<IOrderServices> _LazyOrderService = new Lazy<IOrderServices>(() => new OrderServices(_mapper, _BasketRepository, _unitOfWork));

        public IOrderServices OrderServices => _LazyOrderService.Value;

        #endregion
    }
}
 