using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Contracts.Repository;
using ECommerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _BasketRepository, UserManager<ApplicationUser> userManager) : IServiceManager
    {

        private readonly Lazy<IProductServices> _LazyProductServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));

        public IProductServices ProductServices => _LazyProductServices.Value;

        private readonly Lazy<IBasketServices> _LazyBasketServices = new Lazy<IBasketServices>(() => new BasketServices(_BasketRepository, _mapper));
        public IBasketServices BasketServices => _LazyBasketServices.Value;

        
        private readonly Lazy<IAuthenticationServices> _LazyAuthenticationService = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager));
        public IAuthenticationServices AuthenticationServices => _LazyAuthenticationService.Value;
    }
}
